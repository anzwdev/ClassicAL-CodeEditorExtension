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
using AnZw.NavCodeEditor.Extensions.UI;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for SnippetSelection.xaml
    /// </summary>
    public partial class SnippetSelection : Window
    {

        public SnippetSelectionVM ViewModel
        {
            get { return this.DataContext as SnippetSelectionVM; }
            set { this.DataContext = value; }
        }

        public SnippetSelection()
        {
            InitializeComponent();
            this.Loaded += SnippetSelection_Loaded;
        }

        private void SnippetSelection_Loaded(object sender, RoutedEventArgs e)
        {
            ctSnippets.Focus();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.Selected != null)
                Session.Current.ShowSettings();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
