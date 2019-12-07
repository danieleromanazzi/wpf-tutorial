using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using wpf_challenge_itemscontrol.Command;
using wpf_viewmodel;

namespace wpf_challenge_itemscontrol.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        public ICommand Apply
        {
            get { return GetValue<ICommand>(); }
            set { SetValue(value); }
        }

    }
}
