using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace cgm.AIS.Windows.Controls.Behaviors
{
    public class WindowSizeBehavior
    {
        public static readonly DependencyProperty FloorAreaProperty =
            DependencyProperty.RegisterAttached("FloorArea", typeof(double), typeof(WindowSizeBehavior), new PropertyMetadata(OnFloorAreaChanged));

        public static double GetFloorArea(DependencyObject dependencyObject)
        {
            return (double)dependencyObject.GetValue(FloorAreaProperty);
        }

        public static void SetFloorArea(DependencyObject dependencyObject, double navigate)
        {
            dependencyObject.SetValue(FloorAreaProperty, navigate);
        }

        private static void OnFloorAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                double value = (double)e.NewValue;

                window.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * value);
                window.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * value);
            }
        }
    }
}
