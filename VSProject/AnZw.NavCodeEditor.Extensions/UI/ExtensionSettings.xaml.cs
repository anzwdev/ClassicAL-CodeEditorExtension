using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for ExtensionSettings.xaml
    /// </summary>
    public partial class ExtensionSettings : Window
    {

        public static string SettingsFileMask = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*";

        public SessionSettings Settings
        {
            get { return this.DataContext as SessionSettings; }
        }

        public ExtensionSettings()
        {
            InitializeComponent();
            this.DataContext = Session.Current.Settings;
        }

        private void btnNewSnippet_Click(object sender, RoutedEventArgs e)
        {
            NewSnippet();
        }

        private void btnEditSnippet_Click(object sender, RoutedEventArgs e)
        {
            Snippet snippet = ctSnippets.SelectedItem as Snippet;
            if (snippet != null)
                EditSnippet(snippet);
        }

        private void btnDeleteSnippet_Click(object sender, RoutedEventArgs e)
        {
            Snippet snippet = ctSnippets.SelectedItem as Snippet;
            if (snippet != null)
                DeleteSnippet(snippet);
        }

        protected void NewSnippet()
        {
            Snippet snippet = new Snippet();
            SnippetDetails details = new SnippetDetails();
            details.Settings = this.Settings;
            details.DataContext = snippet;
            bool? result = details.ShowDialog();
            if ((result.HasValue) && (result.Value))
            {
                this.Settings.Snippets.Add(snippet);
            }
        }

        protected void EditSnippet(Snippet snippet)
        {
            Snippet editableSnippet = new Snippet(snippet);
            SnippetDetails details = new SnippetDetails();
            details.Settings = this.Settings;
            details.DataContext = editableSnippet;
            bool? result = details.ShowDialog();
            if ((result.HasValue) && (result.Value))
            {
                snippet.CopyFrom(editableSnippet);
            }
        }

        protected void DeleteSnippet(Snippet snippet)
        {
            if (MessageBox.Show($"Do you want to delete snippet '{snippet.Name}'?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Settings.Snippets.Remove(snippet);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetSettings();
        }

        protected void SaveSettings()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = SettingsFileMask;
            if (saveFileDialog.ShowDialog() == true)
            {
                if (this.Settings.SaveSettings(saveFileDialog.FileName, true))
                    MessageBox.Show("Settings have been saved to file.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        protected void LoadSettings()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = SettingsFileMask;
            if (openFileDialog.ShowDialog() == true)
            {
                SessionSettings newSettings = SessionSettings.LoadSettings(openFileDialog.FileName, false, true);
                if (newSettings != null)
                    this.Settings.CopyFrom(newSettings, false);
            }
        }

        protected void ResetSettings()
        {
            if (MessageBox.Show("Do you want to load global settings? All your changes will be removed.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                this.Settings.CopyFrom(Session.Current.GlobalSettings, false);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void btnDeleteVariable_Click(object sender, RoutedEventArgs e)
        {
            SnippetVariable selectedVariable = ctlParameters.SelectedItem as SnippetVariable;
            if (selectedVariable != null)
                this.Settings.Variables.Remove(selectedVariable);
        }

        private void btnNewVariable_Click(object sender, RoutedEventArgs e)
        {
            SnippetVariable selectedVariable = this.Settings.Variables.AddNew();
            ctlParameters.SelectedItem = selectedVariable;
        }
    }
}
