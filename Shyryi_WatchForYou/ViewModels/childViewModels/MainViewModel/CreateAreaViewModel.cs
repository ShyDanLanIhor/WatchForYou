using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services.ModelServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel
{
    public class CreateAreaViewModel : ViewModelBase
    {
        private string _areaName;
        private string _areaDescription;
        private string _areaInfo;
        private string _areaError;
        private Brush _areaInfoColor;

        private AreaService areaService =
            new AreaService();
        private AriaService userService =
            new AriaService();

        public string AreaName { get => _areaName;
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
        public string AreaError
        {
            get => _areaError;
            set
            {
                _areaError = value;
                OnPropertyChanged(nameof(AreaError));
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
        public ICommand CreateAreaCommand { get; }

        public CreateAreaViewModel()
        {
            CreateAreaCommand = new RelayCommand(
                ExecuteCreateAreaCommand, CanExecuteCreateAreaCommand);
        }

        private bool CanExecuteCreateAreaCommand(object obj)
        {
            bool validData = string.IsNullOrWhiteSpace(
                AreaName) ||
                AreaName.Length < 3 ||
                AreaDescription == null ||
                AreaDescription.Length < 50 ? false : true;
            return validData;
        }

        private async void ExecuteCreateAreaCommand(object obj)
        {
            try
            {
                UserModel user = UserMapper.MapToModel(userService.
                    GetBy(Thread.CurrentPrincipal.Identity.Name));
                areaService.CreateArea(AreaMapper.MapToDto(
                    new AreaModel(AreaName, AreaDescription, user.Id, user)));
                AreaName = "";
                AreaDescription = "";
                AreaInfoColor = (Brush)App.Current.FindResource("CreateAreaColor");
                AreaInfo = "Area was created!";
                await Task.Delay(2000);
                AreaInfo = "";
            }
            catch (Exception)
            {
                AreaInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                AreaInfo = "Area was not created!";
                await Task.Delay(2000);
                AreaInfo = "";
            }
        }
    }
}
