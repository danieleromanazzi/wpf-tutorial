using cgm.AIS.Windows.Controls.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace cgm.AIS.Windows.Controls.Behaviors
{
    public class WebBrowserBehavior
    {
        private static readonly Type OwnerType = typeof(WebBrowserBehavior);

        #region HTML Property
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
                "Html",
                typeof(string),
               OwnerType,
                new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        public static void SetHtml(WebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        static void OnHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = dependencyObject as WebBrowser;
            if (webBrowser != null)
                webBrowser.NavigateToString(e.NewValue as string ?? "&nbsp;");
        }
        #endregion

        #region LocalFileProperty
        public static readonly DependencyProperty LocalFileProperty =
            DependencyProperty.RegisterAttached("LocalFile", typeof(string), OwnerType, new PropertyMetadata(OnLocalFileChanged));

        public static string GetLocalFile(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(LocalFileProperty);
        }

        public static void SetLocalFile(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(LocalFileProperty, value);
        }

        private static void OnLocalFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string url = (string)e.NewValue;
            var webBrowser = (WebBrowser)d;

            if (!FileAssociation.VerifiedFileAssociation(Path.GetExtension(url)))
            {
                //StreamResourceInfo info = Application.GetResourceStream(new Uri("Preview_PDF_Warning.html", UriKind.Relative));
                var c = Assembly.GetExecutingAssembly().GetManifestResourceStream("cgm.Integra.Windows.Resources.Preview_PDF_Warning.html");
                if (c != null)
                    webBrowser.NavigateToStream(c);
                //webBrowser.Navigate(new Uri("about:blank"));
            }
            else
                webBrowser.Navigate(new Uri("file:///" + url));
        }
        #endregion

        #region Navigation Source URL with PostData
        public static readonly DependencyProperty NavigationSourceProperty =
            DependencyProperty.RegisterAttached(
                "NavigationSource",
                typeof(INavigationWebBrowser),
                OwnerType,
                new UIPropertyMetadata(OnNavigationSourcePropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static INavigationWebBrowser GetNavigationSource(DependencyObject obj)
        {
            return (INavigationWebBrowser)obj.GetValue(NavigationSourceProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static void SetNavigationSource(DependencyObject obj, INavigationWebBrowser value)
        {
            obj.SetValue(NavigationSourceProperty, value);
        }
        public static void OnNavigationSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var browser = d as WebBrowser;
                if (browser == null) return;
                if (!(e.NewValue is INavigationWebBrowser) || e.NewValue == null) return;

                System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, f) => true;
                INavigationWebBrowser nav = e.NewValue as INavigationWebBrowser;
                browser.Navigate(nav.Url, "", nav.PostData?.ReadAsByteArrayAsync()?.Result, nav.Headers);
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region DisableJavascriptErrors

        #region SilentJavascriptErrorsContext (private Dependency Property)

        private static readonly DependencyPropertyKey SilentJavascriptErrorsContextKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "SilentJavascriptErrorsContext",
                typeof(SilentJavascriptErrorsContext),
                OwnerType,
                new FrameworkPropertyMetadata(null));

        private static void SetSilentJavascriptErrorsContext(DependencyObject depObj, SilentJavascriptErrorsContext value)
        {
            depObj.SetValue(SilentJavascriptErrorsContextKey, value);
        }

        private static SilentJavascriptErrorsContext GetSilentJavascriptErrorsContext(DependencyObject depObj)
        {
            return (SilentJavascriptErrorsContext)depObj.GetValue(SilentJavascriptErrorsContextKey.DependencyProperty);
        }

        #endregion

        public static readonly DependencyProperty DisableJavascriptErrorsProperty =
            DependencyProperty.RegisterAttached(
                "DisableJavascriptErrors",
                typeof(bool),
                OwnerType,
                new FrameworkPropertyMetadata(OnDisableJavascriptErrorsChangedCallback));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static void SetDisableJavascriptErrors(DependencyObject depObj, bool value)
        {
            depObj.SetValue(DisableJavascriptErrorsProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static bool GetDisableJavascriptErrors(DependencyObject depObj)
        {
            return (bool)depObj.GetValue(DisableJavascriptErrorsProperty);
        }

        private static void OnDisableJavascriptErrorsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = d as WebBrowser;
            if (webBrowser == null) return;
            if (Equals(e.OldValue, e.NewValue)) return;

            var context = GetSilentJavascriptErrorsContext(webBrowser);
            if (context != null)
            {
                context.Dispose();
            }

            if (e.NewValue != null)
            {
                context = new SilentJavascriptErrorsContext(webBrowser);
                SetSilentJavascriptErrorsContext(webBrowser, context);
            }
            else
            {
                SetSilentJavascriptErrorsContext(webBrowser, null);
            }
        }

        private class SilentJavascriptErrorsContext : IDisposable
        {
            private bool? _silent;
            private readonly WebBrowser _webBrowser;


            public SilentJavascriptErrorsContext(WebBrowser webBrowser)
            {
                _silent = new bool?();

                _webBrowser = webBrowser;
                _webBrowser.Loaded += OnWebBrowserLoaded;
                _webBrowser.Navigated += OnWebBrowserNavigated;
            }

            private void OnWebBrowserLoaded(object sender, RoutedEventArgs e)
            {
                if (!_silent.HasValue) return;

                SetSilent();
            }

            private void OnWebBrowserNavigated(object sender, NavigationEventArgs e)
            {
                var webBrowser = (WebBrowser)sender;

                if (!_silent.HasValue)
                {
                    _silent = GetDisableJavascriptErrors(webBrowser);
                }

                if (!webBrowser.IsLoaded) return;

                SetSilent();
            }

            /// <summary>
            /// Solution by Simon Mourier on StackOverflow
            /// http://stackoverflow.com/a/6198700/741414
            /// </summary>
            private void SetSilent()
            {
                _webBrowser.Loaded -= OnWebBrowserLoaded;
                _webBrowser.Navigated -= OnWebBrowserNavigated;

                // get an IWebBrowser2 from the document
                var sp = _webBrowser.Document as IOleServiceProvider;
                if (sp != null)
                {
                    var IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                    var IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                    object webBrowser2;
                    sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser2);
                    if (webBrowser2 != null)
                    {
                        webBrowser2.GetType().InvokeMember(
                            "Silent",
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty,
                            null,
                            webBrowser2,
                            new object[] { _silent });
                    }
                }
            }

            [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            private interface IOleServiceProvider
            {
                [PreserveSig]
                int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
            }

            public void Dispose()
            {
                if (_webBrowser != null)
                {
                    _webBrowser.Loaded -= OnWebBrowserLoaded;
                    _webBrowser.Navigated -= OnWebBrowserNavigated;
                }
            }
        }

        #endregion

        #region BindableSourceProperty Rimosso da usare la Navigation Source
        //public static readonly DependencyProperty BindableSourceProperty =
        //DependencyProperty.RegisterAttached("BindableSource", typeof(string), OwnerType, new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        //public static string GetBindableSource(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(BindableSourceProperty);
        //}

        //public static void SetBindableSource(DependencyObject obj, string value)
        //{
        //    obj.SetValue(BindableSourceProperty, value);
        //}

        //public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    WebBrowser browser = o as WebBrowser;
        //    if (browser != null)
        //    {
        //        string uri = e.NewValue as string;
        //        browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
        //    }
        //}
        #endregion
    }
}
