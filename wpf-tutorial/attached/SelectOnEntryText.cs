using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace wpf_tutorial.attached
{
    public class SelectOnEntryText
    {

        //[AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetSelectOnEntry(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectOnEntryProperty);
        }

        //[AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetSelectOnEntry(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectOnEntryProperty, value);
        }
        
        // Using a DependencyProperty as the backing store for SelectOnEntry.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectOnEntryProperty =
            DependencyProperty.RegisterAttached("SelectOnEntry", typeof(bool), typeof(SelectOnEntryText), new PropertyMetadata(false, SelectOnEntryChanged));

        private static void SelectOnEntryChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (!(bool)args.NewValue) return;
            var text = d as TextBox;
            if (text == null) return;
            text.GotFocus += (s, e) =>
            {
                text.SelectionStart = 0;
                text.SelectionLength = text.Text.Length;
            };
        }

    }
}
