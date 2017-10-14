using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions
{
    public class SessionSettings : ObservableObject
    {

        [XmlIgnore]
        public KeyStateInfo SettingsKeyStateInfo { get; set; }
        [XmlIgnore]
        public KeyStateInfo SnippetSelectionKeyStateInfo { get; set; }

        private string _settingsHotKey;
        public string SettingsHotKey
        {
            get { return _settingsHotKey; }
            set
            {
                this.SetProperty<string>(ref _settingsHotKey, value);
                this.SettingsKeyStateInfo.SetHotKey(_settingsHotKey);
            }
        }

        private string _snippetSelectionHotKey;
        public string SnippetSelectionHotKey
        {
            get { return _snippetSelectionHotKey; }
            set
            {
                this.SetProperty<string>(ref _snippetSelectionHotKey, value);
                this.SnippetSelectionKeyStateInfo.SetHotKey(_snippetSelectionHotKey);
            }
        }

        public bool EnableXmlDocumentation { get; set; }

        public bool AutoCloseElements { get; set; }

        public bool DetectWordsInNames { get; set; }

        public bool SetZoom { get; set; }

        public double Zoom { get; set; }

        public BindingList<Snippet> Snippets { get; }
        public BindingList<SnippetVariable> Variables { get; }

        public SessionSettings()
        {
            this.Snippets = new BindingList<Snippet>();
            this.Variables = new BindingList<SnippetVariable>();
            this.SettingsKeyStateInfo = new KeyStateInfo();
            this.SnippetSelectionKeyStateInfo = new KeyStateInfo();
            Clear();
        }

        public SessionSettings(SessionSettings source) : this()
        {
            CopyFrom(source, false);
        }

        public void Clear()
        {
            this.SnippetSelectionHotKey = "Ctrl+Shift+T";
            this.SettingsHotKey = "Ctrl+Shift+E";

            this.EnableXmlDocumentation = true;
            this.AutoCloseElements = false;
            this.DetectWordsInNames = true;
            this.SetZoom = false;
            this.Zoom = 100;
            this.Snippets.Clear();
            this.Variables.Clear();
        }

        protected Snippet FindSnippet(string name)
        {
            foreach (Snippet snippet in this.Snippets)
                if (snippet.Name == name)
                    return snippet;
            return null;
        }

        protected SnippetVariable FindVariable(string name)
        {
            foreach (SnippetVariable variable in this.Variables)
                if (variable.Name == name)
                    return variable;
            return null;
        }

        public void CopyFrom(SessionSettings source, bool append)
        {
            if (!append)
            {
                this.Snippets.Clear();
                this.Variables.Clear();
            }

            this.SettingsHotKey = source.SettingsHotKey;
            this.SnippetSelectionHotKey = source.SnippetSelectionHotKey;
            this.EnableXmlDocumentation = source.EnableXmlDocumentation;
            this.AutoCloseElements = source.AutoCloseElements;
            this.DetectWordsInNames = source.DetectWordsInNames;
            this.SetZoom = source.SetZoom;
            this.Zoom = source.Zoom;

            //append snippets
            foreach (Snippet sourceSnippet in source.Snippets)
            {
                this.Snippets.Add(new Snippet(sourceSnippet));
            }

            //append variables
            foreach (SnippetVariable sourceVariable in source.Variables)
            {
                this.Variables.Add(new SnippetVariable(sourceVariable));
            }

        }

        #region Load settings

        public static SessionSettings LoadSettings(string fileName, bool createIfNotFound, bool displayError = false)
        {
            try
            {

                if (File.Exists(fileName))
                {
                    XmlReader xmlReader = XmlReader.Create(fileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(SessionSettings));
                    SessionSettings settings = serializer.Deserialize(xmlReader) as SessionSettings;
                    xmlReader.Close();
                    if (settings != null)
                        return settings;
                }
            }
            catch (Exception e)
            {
                if (displayError)
                    System.Windows.MessageBox.Show("Settings file cannot be loaded. " + e.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            if (createIfNotFound)
                return new SessionSettings();
            return null;
        }

        public bool SaveSettings(string fileName, bool displayError)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(SessionSettings));
                serializer.Serialize(xmlWriter, this);
                xmlWriter.Close();
            }
            catch (Exception e)
            {
                if (displayError)
                    System.Windows.MessageBox.Show($"Saving settings to file {fileName} failed. Error message: {e.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        #endregion

    }
}
