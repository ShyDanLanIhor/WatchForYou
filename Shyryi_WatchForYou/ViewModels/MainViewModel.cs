using FontAwesome.Sharp;
using InfinityStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories.IRepositories;
using Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.DTOs;

namespace Shyryi_WatchForYou.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // fields
        private UserAccountEntity _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private UserService userService = new UserService();

        public UserAccountEntity CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

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

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCreateAreaViewCommand { get; }
        public ICommand ShowAreasListViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public MainViewModel()
        {
            UserDto user = userService.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                CurrentUserAccount = new UserAccountEntity(user.Username, $"Welcome" +
                        $"{(!string.IsNullOrEmpty(user.FirstName) ? " " + user.FirstName : "")}" +
                        $"{(!string.IsNullOrEmpty(user.LastName) ? " " + user.LastName : "")}");
            }
            else { MessageBox.Show("Invalid user, not logged in"); }

            ShowHomeViewCommand = new RelayCommand(ExecuteShowHomeViewCommand);
            ShowCreateAreaViewCommand = new RelayCommand(ExecuteCreateAreaViewCommand);
            ShowAreasListViewCommand = new RelayCommand(ExecuteShowAreasListViewCommand);
            ShowSettingsViewCommand = new RelayCommand(ExecuteShowSettingsViewCommand);
        }
        
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Home Page";
            Icon = IconChar.Home;
        }
        private void ExecuteCreateAreaViewCommand(object obj)
        {
            CurrentChildView = new CreateAreaViewModel();
            Caption = "Create Area Page";
            Icon = IconChar.BuildingUser;
        }
        private void ExecuteShowAreasListViewCommand(object obj)
        {
            CurrentChildView = new AreasListViewModel();
            Caption = "Areas List Page";
            Icon = IconChar.BuildingCircleArrowRight;
        }
        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new SettingViewModel(this);
            Caption = "Settings Page";
            Icon = IconChar.Gear;
        }
    }
}
