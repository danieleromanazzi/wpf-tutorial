using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpf_tutorial.ViewModels;
using wpf_tutorial_MVVM.Model;

namespace wpf_tutorial_MVVM.ViewModel
{
    public class CredentialsViewModel : ViewModelBase
    {
        internal Credentials Model { get; private set; }
        public CredentialsViewModel()
        {
            Model = new Credentials();
            Model.Load();

            Submit = new DelegateCommand()
            {
                ExecuteDelegate = o =>
                {
                    Model.Save(Model.Username, NewPassword);
                },
                CanExecuteDelegate = o => Verify(),
            };

            Cancel = new DelegateCommand()
            {
                ExecuteDelegate = o =>
                {
                    ((Window)o).Close();
                },
                CanExecuteDelegate = o =>true,
            };

            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Username))
            {
                
            }
            if (e.PropertyName == nameof(OldPassword))
            {

            }
            if (e.PropertyName == nameof(NewPassword))
            {

            }
        }

        private bool Verify()
        {
            bool lenghtCheck = !string.IsNullOrEmpty(OldPassword) &&
                               !string.IsNullOrEmpty(NewPassword) && 
                               !string.IsNullOrEmpty(ConfirmPassword);

            if (!lenghtCheck)
            {
                return false;
            }

            bool equal = NewPassword == ConfirmPassword;

            if (!equal)
            {
                return false;
            }

            return true;
        }


        public string Username
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string OldPassword
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string NewPassword
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        
        public string ConfirmPassword
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Message
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public ICommand Submit
        {
            get { return GetValue<ICommand>(); }
            set { SetValue(value); }
        }

        public ICommand Cancel
        {
            get { return GetValue<ICommand>(); }
            set { SetValue(value); }
        }

    }
}
