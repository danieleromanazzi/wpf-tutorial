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

namespace wpf_challenge_databinding.View
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
        }



        public string SettingName
        {
            get { return (string)GetValue(SettingNameProperty); }
            set { SetValue(SettingNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingNameProperty =
            DependencyProperty.Register("SettingName", typeof(string), typeof(Setting), new PropertyMetadata(""));



        public string SettingDescription
        {
            get { return (string)GetValue(SettingDescriptionProperty); }
            set { SetValue(SettingDescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingDescription.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingDescriptionProperty =
            DependencyProperty.Register("SettingDescription", typeof(string), typeof(Setting), new PropertyMetadata(""));


        public bool SettingValue
        {
            get { return (bool)GetValue(SettingValueProperty); }
            set { SetValue(SettingValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingValueProperty =
            DependencyProperty.Register("SettingValue", typeof(bool), typeof(Setting), new PropertyMetadata(false));

        
    }
}
