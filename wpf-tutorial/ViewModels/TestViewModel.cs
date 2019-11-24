using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tutorial.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string myVar;

        public string MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


    }
}
