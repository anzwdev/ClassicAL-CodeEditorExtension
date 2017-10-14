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

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    /// <summary>
    /// Interaction logic for RecordFieldListCodeGeneratorWindow.xaml
    /// </summary>
    public partial class RecordFieldListCodeGeneratorWindow : Window
    {

        public RecordFieldListCodeGeneratorVM ViewModel
        {
            get { return this.DataContext as RecordFieldListCodeGeneratorVM; }
            set { this.DataContext = value; }
        }

        public RecordFieldListCodeGeneratorWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.SetSelectedFields(lstFields.SelectedItems);
            this.DialogResult = true;
        }

    }
}
