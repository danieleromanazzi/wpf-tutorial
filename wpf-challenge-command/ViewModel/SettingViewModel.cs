using System;
using System.Collections.Generic;
using System.Text;
using wpf_viewmodel;

namespace wpf_challenge_command.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {
            Name = "Abilita tema scuro";
            Description = "Questa impostazione applica un tema scuro alla tua applicazione";
            Value = true;
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool Value
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
    }
}
