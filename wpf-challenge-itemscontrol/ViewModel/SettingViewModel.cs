using System;
using System.Collections.Generic;
using System.Text;
using wpf_viewmodel;

namespace wpf_challenge_itemscontrol.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {

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
