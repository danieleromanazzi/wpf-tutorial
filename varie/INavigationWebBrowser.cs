using System.Net.Http;

namespace cgm.AIS.Windows.Controls.Behaviors
{
    public interface INavigationWebBrowser
    {
        string Url { get; set; }

        FormUrlEncodedContent PostData { get; set; }

        string Headers { get; set; }
    }
}
