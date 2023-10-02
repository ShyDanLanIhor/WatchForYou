using FontAwesome.Sharp;
using InfinityStudio.Models;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel;
using System;
using System.Threading;
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class MainAreaViewModel : ViewModelBase
    {
        private UserAccountEntity _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private string _areaName;
        private string _titleName;

        public static event Action ViewClosed;
        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public string AreaName
        {
            get => _areaName;
            set
            {
                _areaName = value;
                OnPropertyChanged(nameof(AreaName));
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

        public ICommand ShowAriaInfoViewCommand { get; }
        public ICommand ShowCreateThingViewCommand { get; }
        public ICommand ShowThingsListViewCommand { get; }

        private int currentId;
        public MainAreaViewModel(int areaId)
        {
            currentId = areaId;
            AreaDto currentArea = AreaService.GetAreaById(areaId);
            AreaName = currentArea.Name;
            CurrentChildView = new AreaInfoViewModel(areaId);
            Caption = "Current Area Info Page";
            Icon = IconChar.Info;
            TitleName = currentArea.Name + " view";

            ShowAriaInfoViewCommand = new RelayCommand(ExecuteShowAriaInfoViewCommand);
            ShowCreateThingViewCommand = new RelayCommand(ExecuteShowCreateThingViewCommand);
            ShowThingsListViewCommand = new RelayCommand(ExecuteShowThingsListViewCommand);

            AreaInfoViewModel.AreaChanged += (areaId) => AreaChanged(areaId);
            AreasListViewModel.AreaIsDeleted += (areaId) =>
            {
                if (areaId == currentArea.Id) { ViewClosed?.Invoke(); }
            };
        }

        private void AreaChanged(int areaId)
        {
            currentId = areaId;
            AreaName = AreaService.GetAreaById(areaId).Name;
        }

        private void ExecuteShowAriaInfoViewCommand(object obj)
        {
            CurrentChildView = new AreaInfoViewModel(currentId);
            Caption = "Current Area Info Page";
            Icon = IconChar.Info;
        }
        private void ExecuteShowCreateThingViewCommand(object obj)
        {
            CurrentChildView = new CreateThingViewModel(currentId);
            Caption = "Connect Thing Page";
            Icon = IconChar.Camera;
        }
        private void ExecuteShowThingsListViewCommand(object obj)
        {
            CurrentChildView = new ThingsListViewModel(currentId);
            Caption = "Things List Page";
            Icon = IconChar.List;
        }
    }
}
