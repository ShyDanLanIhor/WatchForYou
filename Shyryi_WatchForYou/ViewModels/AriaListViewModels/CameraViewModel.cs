using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories;
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
        private string _titleName;

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

        public string TitleName
        {
            get { return _titleName; }
            set
            {
                if (_titleName != value)
                {
                    _titleName = value;
                    OnPropertyChanged(nameof(TitleName));
                }
            }
        }

        public CameraViewModel(ThingDto thing)
        {
            TitleName = AreaRepository.GetAreaById(thing.AreaId).Name + " " + 
                thing.Name.ToLower() + " view.";
            webViewUrl = new Uri("http://" + thing.Ip);
        }

    }
}
