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

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for VariableSelection.xaml
    /// </summary>
    public partial class VariableSelection : Window
    {

        public VariableSelectionVM ViewModel
        {
            get { return this.DataContext as VariableSelectionVM; }
            set { this.DataContext = value; }
        }

        public VariableSelection()
        {
            InitializeComponent();
            this.Loaded += VariableSelection_Loaded;
        }

        private void VariableSelection_Loaded(object sender, RoutedEventArgs e)
        {
            ctVariables.Focus();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Selected != null)
                this.DialogResult = true;
        }

        private void ctVariables_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.Selected != null)
                this.DialogResult = true;
        }
    }
}
