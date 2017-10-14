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
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for SnippetDetails.xaml
    /// </summary>
    public partial class SnippetDetails : Window
    {

        private SessionSettings _settings;
        public SessionSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                if (_snippetManager != null)
                    _snippetManager.Settings = _settings;
            }
        }

        private SnippetManager _snippetManager;
        public SnippetManager SnippetManager
        {
            get
            {
                if (_snippetManager == null)
                    _snippetManager = new SnippetManager(this.Settings);
                return _snippetManager;
            }
        }

        public Snippet Snippet
        {
            get { return this.DataContext as Snippet; }
        }

        public SnippetDetails()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void OnInsertVariable(Object sender, ExecutedRoutedEventArgs e)
        {
            InsertVariable();
        }

        private void OnTestSnippet(Object sender, ExecutedRoutedEventArgs e)
        {
            TestSnippet();
        }

        protected void InsertVariable()
        {
            VariableSelection selection = new VariableSelection();
            selection.ViewModel = new VariableSelectionVM(this.SnippetManager);

            if (selection.ShowDialog() == true)
            {
                if (selection.ViewModel.Selected != null)
                    txtContent.SelectedText = "{{" + selection.ViewModel.Selected.Name + "}}";
            }
        }

        protected void TestSnippet()
        {
            Snippet editedSnippet = this.Snippet;
            if (editedSnippet != null)
            {
                try
                {
                    string text = this.SnippetManager.ParseSnippet(editedSnippet, 0, null);
                    MessageBox.Show(text, "Snippet", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Snippet parsing error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
