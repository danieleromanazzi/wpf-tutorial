using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wpf_challenge_listbox.Command;
using wpf_challenge_listbox.Model;
using wpf_viewmodel;

namespace wpf_challenge_listbox.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            Items = new ObservableCollection<SettingViewModel>();
            var settings = Settings.LoadFromResource();
            foreach (var item in settings)
            {
                Items.Add(
                    new SettingViewModel
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Value = item.Value,
                        Apply = CommandFactory.Curret.Create(item.CommandName)
                    });
            }
        }

        public ObservableCollection<SettingViewModel> Items
        {
            get { return GetValue<ObservableCollection<SettingViewModel>>(); }
            set { SetValue(value); }
        }
    }
}
