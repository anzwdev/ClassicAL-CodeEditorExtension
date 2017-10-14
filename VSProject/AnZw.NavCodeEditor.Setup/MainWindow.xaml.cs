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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnZw.NavCodeEditor.Setup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindowVM ViewModel
        {
            get { return this.DataContext as MainWindowVM; }
            set { this.DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.ViewModel = new MainWindowVM();
        }

        private void btnShowSettings_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Session.ShowSettings();
        }

    }
}
