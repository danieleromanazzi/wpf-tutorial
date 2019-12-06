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

namespace wpf_tutorial_attachedproperty
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

        bool Light = false;
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string theme = "LightTheme";
            if (Light)
            {
                theme = "DarkTheme";
                Light = false;
            }
            else
            {
                Light = true;
            }
            ResourceDictionary newRes = new ResourceDictionary();
            newRes.Source = new Uri($"/wpf-tutorial-attachedproperty;component/Resources/{theme}.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newRes);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility =  Visibility.Collapsed;
        }
    }
}
