using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Repositories;
using Shyryi_WatchForYou.Services.ModelServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class CreateThingViewModel : ViewModelBase
    {
        private string _thingName;
        private string _thingDescription;
        private string _thingIp;
        private string _thingInfo;
        private string _thingError;
        private bool _isVideo;
        private Brush _thingInfoColor;


        public string ThingName
        {
            get => _thingName;
            set
            {
                _thingName = value;
                OnPropertyChanged(nameof(ThingName));
            }
        }
        public string ThingDescription
        {
            get => _thingDescription;
            set
            {
                _thingDescription = value;
                OnPropertyChanged(nameof(ThingDescription));
            }
        }
        public string ThingIp
        {
            get => _thingIp;
            set
            {
                _thingIp = value;
                OnPropertyChanged(nameof(ThingIp));
            }
        }
        public string ThingInfo
        {
            get => _thingInfo;
            set
            {
                _thingInfo = value;
                OnPropertyChanged(nameof(ThingInfo));
            }
        }
        public string ThingError
        {
            get => _thingError;
            set
            {
                _thingError = value;
                OnPropertyChanged(nameof(ThingError));
            }
        }
        public bool IsVideo
        {
            get => _isVideo;
            set
            {
                _isVideo = value;
                OnPropertyChanged(nameof(IsVideo));
            }
        }
        public Brush ThingInfoColor
        {
            get => _thingInfoColor;
            set
            {
                _thingInfoColor = value;
                OnPropertyChanged(nameof(ThingInfoColor));
            }
        }
        public ICommand CreateThingCommand { get; }
        public int areaId;
        public CreateThingViewModel(int areaId)
        {
            this.areaId = areaId;
            CreateThingCommand = new RelayCommand(
                ExecuteCreateThingCommand, CanExecuteCreateThingCommand);
        }

        private bool CanExecuteCreateThingCommand(object obj)
        {
            bool validData = string.IsNullOrWhiteSpace(
                ThingName) ||
                ThingName.Length < 3 ||
                ThingDescription == null ||
                ThingDescription.Length < 50 ? false : true;
            return validData;
        }

        private async void ExecuteCreateThingCommand(object obj)
        {
            try
            {

                ThingService.CreateThing(ThingMapper.MapToDto(new ThingModel(
                    ThingName, ThingIp, IsVideo, false, ThingDescription, areaId,
                    AreaMapper.MapToModel(AreaService.GetAreaById(areaId)))));
                IsVideo = false;
                ThingName = "";
                ThingDescription = "";
                ThingInfoColor = (Brush)App.Current.FindResource("ConnectDeviceColor");
                ThingInfo = "Device was connected!";
                await Task.Delay(2000);
                ThingInfo = "";
            }
            catch (Exception)
            {
                ThingInfoColor = (Brush)App.Current.FindResource("ConnectDeviceColor");
                ThingInfo = "Device was not connected!";
                await Task.Delay(2000);
                ThingInfo = "";
            }
        }
    }
}
