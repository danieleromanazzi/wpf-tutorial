using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace cgm.AIS.Windows.Controls.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName, object value = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                if (value == null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
                else
                    handler(value, new PropertyChangedEventArgs(propertyName));
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(string propertyName, T value)
        {
            if (!properties.ContainsKey(propertyName))
            {
                properties.Add(propertyName, default(T));
            }

            var oldValue = GetValue<T>(propertyName);
            if (!EqualityComparer<T>.Default.Equals(oldValue, value))
            {
                properties[propertyName] = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected T GetValue<T>(string propertyName)
        {
            if (!properties.ContainsKey(propertyName))
            {
                return default(T);
            }
            else
            {
                return (T)properties[propertyName];
            }
        }

    }
}
