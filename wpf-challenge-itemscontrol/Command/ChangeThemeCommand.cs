using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace wpf_challenge_itemscontrol.Command
{
    public class ChangeThemeCommand : DelegateCommand
    {
        bool Light = false;
        public ChangeThemeCommand()
        {
            ExecuteDelegate = o => {
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
                newRes.Source = new Uri($"/Resources/{theme}.xaml", UriKind.RelativeOrAbsolute);
                //ContentControl view = (ContentControl)o;
                var view = Application.Current.MainWindow;
                view.Resources.MergedDictionaries.Clear();
                view.Resources.MergedDictionaries.Add(newRes);
            };
            CanExecuteDelegate = o => true;
        }
    }
}
