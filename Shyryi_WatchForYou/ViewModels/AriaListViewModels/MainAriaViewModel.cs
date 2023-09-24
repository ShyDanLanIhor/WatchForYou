using FontAwesome.Sharp;
using InfinityStudio.Models;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class MainAriaViewModel : ViewModelBase
    {
        private UserAccountEntity _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private string _thingName;
        private AreaDto currentArea;

        private AriaService areaService = new AriaService();

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
        public string ThingName
        {
            get => _thingName;
            set
            {
                _thingName = value;
                OnPropertyChanged(nameof(ThingName));
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

        public ICommand ShowAriaInfoViewCommand { get; }
        public ICommand ShowCreateThingViewCommand { get; }
        public ICommand ShowThingsListViewCommand { get; }

        public MainAriaViewModel(AreaDto aria)
        {
            ThingName = aria.Name;
            currentArea = aria;

            ShowAriaInfoViewCommand = new RelayCommand(ExecuteShowAriaInfoViewCommand);
            ShowCreateThingViewCommand = new RelayCommand(ExecuteShowCreateThingViewCommand);
            ShowThingsListViewCommand = new RelayCommand(ExecuteShowThingsListViewCommand);
        }

        private void ExecuteShowAriaInfoViewCommand(object obj)
        {
            CurrentChildView = new AriaInfoViewModel(currentArea);
            Caption = "Current Area Info Page";
            Icon = IconChar.Info;
        }
        private void ExecuteShowCreateThingViewCommand(object obj)
        {
            CurrentChildView = new CreateThingViewModel(currentArea);
            Caption = "Connect Thing Page";
            Icon = IconChar.Camera;
        }
        private void ExecuteShowThingsListViewCommand(object obj)
        {
            CurrentChildView = new ThingsListViewModel(currentArea);
            Caption = "Things List Page";
            Icon = IconChar.List;
        }
    }
}
