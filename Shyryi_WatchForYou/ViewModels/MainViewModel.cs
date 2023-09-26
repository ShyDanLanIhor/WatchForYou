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
using Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.DTOs;
using Microsoft.VisualBasic.ApplicationServices;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System.Security.Principal;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Data;

namespace Shyryi_WatchForYou.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // fields
        private UserAccountEntity _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

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


        private CancellationTokenSource cancellationTokenSource;
        private string currentUserName;

        public MainViewModel()
        {
            currentUserName = Thread.CurrentPrincipal.Identity.Name;
            UserDto user = UserService.GetByUsername(currentUserName);

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

            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => CheckIfAlerted(cancellationTokenSource.Token), cancellationTokenSource.Token);
        }
        private async Task CheckIfAlerted(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                UserDto user = UserService.GetByUsername(currentUserName);
                currentUserName = Thread.CurrentPrincipal.Identity.Name;

                DbContextService.RefreshDataFromDatabase();
                List<ThingDto> things = UserService.GetAllThingsByUser(user.Id);
                foreach (var thing in things)
                {
                    if (thing.IsAlerted)
                    {
                        MessageBox.Show($"Thing {thing.Id} is alerted.");
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(240), cancellationToken);
            }
        }
        public void StopChecking()
        {
            cancellationTokenSource.Cancel();
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
