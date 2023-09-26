using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Services.ModelServices;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class CameraViewModel : ViewModelBase
    {
        private Uri webViewUrl;

        public Uri WebViewUrl
        {
            get { return webViewUrl; }
            set
            {
                if (webViewUrl != value)
                {
                    webViewUrl = value;
                    OnPropertyChanged(nameof(WebViewUrl));
                }
            }
        }
        public static string currentWebViewUrl;

        public CameraViewModel()
        {
            webViewUrl = new Uri("http://" + currentWebViewUrl);
        }

    }
}
