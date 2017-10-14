using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.UI;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions
{
    public class Session
    {

        #region Static current instance

        private static Session _current = null;
        public static Session Current
        {
            get
            {
                if (_current == null)
                    _current = new Session();
                return _current;
            }
        }

        #endregion

        public SessionSettings Settings { get; private set; }
        public SnippetManager SnippetManager { get; }

        public SessionSettings GlobalSettings { get; private set; }

        public Session()
        {
            this.Settings = new SessionSettings();
            this.SnippetManager = new SnippetManager(this);
            LoadSettings();
        }

        public bool ShowSettings()
        {
            SessionSettings editableSettings = new SessionSettings(this.Settings);
            ExtensionSettings settingsWindow = new ExtensionSettings();
            settingsWindow.DataContext = editableSettings;
            bool? result = settingsWindow.ShowDialog();
            if ((result.HasValue) && (result.Value))
            {
                this.Settings.CopyFrom(editableSettings, false);
                SaveSettings();
                return true;
            }
            return false;
        }

        public void CreateDefaultSnippets()
        {
            this.Settings.Snippets.Add(new Snippet() { Name = "FFOR", Description = "FOR loop template", Content = "FOR <<variable>> := 1 TO <<counter>> DO BEGIN\n  {{Selection}}\nEND;\n", HotKey = "Ctrl+Shift+I" });
            this.Settings.Snippets.Add(new Snippet() { Name = "RREPEAT", Description = "REPEAT UNTIL loop template", Content = "REPEAT\n  {{Selection}}\nUNTIL (<<condition>>);" });
            this.Settings.Snippets.Add(new Snippet() { Name = "//-", Description = "Start Modification Comment", Content = "//DOC {{Project}} {{Date}} {{User}} >>" });
            this.Settings.Snippets.Add(new Snippet() { Name = "//+", Description = "End Modification Comment", Content = "//DOC {{Project}} {{Date}} {{User}} <<" });
            this.Settings.Snippets.Add(new Snippet() { Name = "DDATABASE::", Description = "Convert number to DATABASE::number", Content = "DATABASE::\"{{Selection}}\"", HotKey = "Ctrl+Shift+D" });

            this.Settings.Variables.Add(new SnippetVariable() { Name = "User", Description = "Current user initials", Value = "AZ" });
            this.Settings.Variables.Add(new SnippetVariable() { Name = "Project", Description = "Code of current project/modification", Value = "OP06984" });
        }

        #region Loading and saving settings

        protected static string SettingsFileName = "AnZw.NavCodeEditor.Extensions.xml";

        protected string GlobalSettingsPath
        {
            get { return Path.Combine(DirectoryHelper.CurrentAssemblyPath(), SettingsFileName); }
        }

        protected string UserSettingsPath
        {
            get { return Path.Combine(DirectoryHelper.GetApplicationDataPath(), SettingsFileName); }
        }

        public void LoadSettings()
        {
            this.GlobalSettings = SessionSettings.LoadSettings(GlobalSettingsPath, true);
            this.Settings = SessionSettings.LoadSettings(UserSettingsPath, false);
            if (this.Settings == null)
                this.Settings = new SessionSettings(this.GlobalSettings);
        }

        public void SaveSettings()
        {
            this.Settings.SaveSettings(UserSettingsPath, true);
        }

        #endregion

    }
}
