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
    /// Interaction logic for RecordAssignmentCodeGeneratorWindow.xaml
    /// </summary>
    public partial class RecordAssignmentCodeGeneratorWindow : Window
    {

        public RecordAssignmentCodeGeneratorVM ViewModel
        {
            get { return this.DataContext as RecordAssignmentCodeGeneratorVM; }
            set { this.DataContext = value; }
        }

        public RecordAssignmentCodeGeneratorWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
