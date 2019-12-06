using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_viewmodel
{
    //http://www.fabiocozzolino.eu/my-viewmodel-base-class-implementation/
    public class ViewModelExample : ViewModelBase
    {
        //private string _title;
        //public string Title
        //{
        //    get
        //    {
        //        return _title;
        //    }
        //    set
        //    {
        //        if (value != _title)
        //        {
        //            _title = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}


        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


    }
}