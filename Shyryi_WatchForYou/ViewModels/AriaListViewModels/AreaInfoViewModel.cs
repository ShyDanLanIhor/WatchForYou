using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Services.ModelServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class AreaInfoViewModel : ViewModelBase
    {
        private string _areaName;
        private string _areaDescription;
        private string _areaInfo;
        private Brush _areaInfoColor;

        public static event Action AreasListChanged;
        public static event Action<int> AreaChanged;

        public string AreaName
        {
            get => _areaName;
            set
            {
                _areaName = value;
                OnPropertyChanged(nameof(AreaName));
            }
        }
        public string AreaDescription
        {
            get => _areaDescription;
            set
            {
                _areaDescription = value;
                OnPropertyChanged(nameof(AreaDescription));
            }
        }
        public string AreaInfo
        {
            get => _areaInfo;
            set
            {
                _areaInfo = value;
                OnPropertyChanged(nameof(AreaInfo));
            }
        }
        public Brush AreaInfoColor
        {
            get => _areaInfoColor;
            set
            {
                _areaInfoColor = value;
                OnPropertyChanged(nameof(AreaInfoColor));
            }
        }
        public ICommand ChangeAreaCommand { get; }

        private int currentId;
        public AreaInfoViewModel(int areaId)
        {
            currentId = areaId;
            AreaDto currentArea = AreaService.GetAreaById(currentId);
            AreaName = currentArea.Name;
            AreaDescription = currentArea.Description;
            ChangeAreaCommand = new RelayCommand(
                ExecuteChangeAreaCommand, CanExecuteChangeAreaCommand);
        }

        private bool CanExecuteChangeAreaCommand(object obj)
        {
            bool validData = string.IsNullOrWhiteSpace(
                AreaName) ||
                AreaName.Length < 3 ? false : true;
            return validData;
        }

        private async void ExecuteChangeAreaCommand(object obj)
        {
            try
            {
                AreaDto currentArea = AreaService.GetAreaById(currentId);
                bool first = AreaService.CheckIfAreaExist(AreaName, Thread.CurrentPrincipal.Identity.Name);
                bool second = AreaName != currentArea.Name;
                if (first && second)
                { throw new ExistingDataException("Area name already exist!"); }
                AreaService.UpdateArea(currentArea.Id, AreaMapper.MapToDto( new AreaModel(
                    currentArea.Id, AreaName, AreaDescription, currentArea.UserId, UserMapper.MapToModel(
                    UserService.GetByUsername(Thread.CurrentPrincipal.Identity.Name)))));

                AreasListChanged?.Invoke();
                AreaChanged?.Invoke(currentArea.Id);

                AreaInfoColor = (Brush)App.Current.FindResource("AreaInfoColor");
                AreaInfo = "Area was changed!";
                await Task.Delay(2000);
                AreaInfo = "";
            }
            catch (Exception ex)
            {
                (AreaInfoColor, AreaInfo) =
                    ExceptionService.HandleGUIException(ex);
                await Task.Delay(2000);
                AreaInfo = "";
            }
        }
    }
}
