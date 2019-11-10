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
using wpf_tutorial.WPF_versus_WinForms;

namespace wpf_tutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenForm(object sender, RoutedEventArgs e)
        {
            var form = new Win32Form();
            form.Show();
        }

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            var window = new WPFWindow();
            window.Show();
        }
    }
}
