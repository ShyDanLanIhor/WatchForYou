using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class AriaInfoViewModel : ViewModelBase
    {
        private string _areaName;
        private string _areaDescription;
        private string _areaInfo;
        private string _areaError;
        private Brush _areaInfoColor;
        private AreaDto currentArea;

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
        public AreaDto CurrentArea
        {
            get { return currentArea; }
            set
            {
                currentArea = value;
                OnPropertyChanged(nameof(CurrentArea));
            }
        }
        public ICommand ChangeAreaCommand { get; }
        MainAriaViewModel currentMainAriaViewModel;

        public AriaInfoViewModel(MainAriaViewModel mainAriaViewModel, int areaId)
        {
            this.currentMainAriaViewModel = mainAriaViewModel;
            CurrentArea = AreaService.GetAreaById(areaId);
            AreaName = CurrentArea.Name;
            AreaDescription = CurrentArea.Description;
            ChangeAreaCommand = new RelayCommand(
                ExecuteChangeAreaCommand, CanExecuteChangeAreaCommand);
        }

        private bool CanExecuteChangeAreaCommand(object obj)
        {
            bool validData = string.IsNullOrWhiteSpace(
                AreaName) ||
                AreaName.Length < 3 ||
                AreaDescription == null ||
                AreaDescription.Length < 50 ? false : true;
            return validData;
        }

        private async void ExecuteChangeAreaCommand(object obj)
        {
            try
            {
                AreaService.UpdateArea(CurrentArea.Id, AreaMapper.MapToDto( new AreaModel(
                    CurrentArea.Id, AreaName, AreaDescription, CurrentArea.UserId, UserMapper.MapToModel(
                    UserService.GetByUsername(Thread.CurrentPrincipal.Identity.Name)))));
                this.currentMainAriaViewModel.AreaName = AreaName;
                AreaInfoColor = (Brush)App.Current.FindResource("AreaInfoColor");
                AreaInfo = "Area was changed!";
                await Task.Delay(2000);
                AreaInfo = "";
            }
            catch (Exception)
            {
                AreaInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                AreaInfo = "Area was not changed!";
                await Task.Delay(2000);
                AreaInfo = "";
            }
        }
    }
}
