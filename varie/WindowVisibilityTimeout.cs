using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace cgm.AIS.Windows.Controls.Behaviors
{
    public class WindowVisibilityTimeout
    {
        public static readonly DependencyProperty VisibilityTimeoutProperty =
            DependencyProperty.RegisterAttached("VisibilityTimeout", typeof(int), typeof(WindowVisibilityTimeout), new PropertyMetadata(OnVisibilityTimeoutChanged));

        public static int GetVisibilityTimeout(DependencyObject dependencyObject)
        {
            return (int)dependencyObject.GetValue(VisibilityTimeoutProperty);
        }

        public static void SetVisibilityTimeout(DependencyObject dependencyObject, int navigate)
        {
            dependencyObject.SetValue(VisibilityTimeoutProperty, navigate);
        }

        private static void OnVisibilityTimeoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                window.Visibility = System.Windows.Visibility.Visible;
                int value = (int)e.NewValue;

                Common.DispatcherTimeout.Timeout(
                    DispatcherPriority.Normal,
                    TimeSpan.FromSeconds(2.0),
                    timeout =>
                    {
                        window.Visibility = System.Windows.Visibility.Collapsed;
                        SetVisibilityTimeout(d, 0);
                    });
            }
        }
    }
}
