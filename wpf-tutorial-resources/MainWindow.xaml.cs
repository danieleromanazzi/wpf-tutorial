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

namespace wpf_tutorial_resources
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Collapsed;
        }

        bool Light = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
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
            newRes.Source = new Uri($"/wpf-tutorial-resources;component/Resources/{theme}.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newRes);

        }

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
            newRes.Source = new Uri($"/wpf-tutorial-resources;component/Resources/{theme}.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newRes);
        }
    }
}
