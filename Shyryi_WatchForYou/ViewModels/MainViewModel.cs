using FontAwesome.Sharp;
using InfinityStudio.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using System;
using Microsoft.VisualBasic.ApplicationServices;

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
            CurrentChildView = new HomeViewModel();
            Caption = "Home Page";
            Icon = IconChar.Home;
            if (user != null)
            {
                CurrentUserAccount = new UserAccountEntity(user.Username, $"Welcome" +
                        $"{(!string.IsNullOrEmpty(user.FirstName) ? " " + user.FirstName : "")}" +
                        $"{(!string.IsNullOrEmpty(user.LastName) ? " " + user.LastName : "")}");
            }
            else { MessageBoxViewModel.Show("Invalid user, not logged in"); }

            ShowHomeViewCommand = new RelayCommand(ExecuteShowHomeViewCommand);
            ShowCreateAreaViewCommand = new RelayCommand(ExecuteCreateAreaViewCommand);
            ShowAreasListViewCommand = new RelayCommand(ExecuteShowAreasListViewCommand);
            ShowSettingsViewCommand = new RelayCommand(ExecuteShowSettingsViewCommand);

            SettingViewModel.UserDataChanged += UserDataChanged;

            //Task.Run(CheckIfAlerted);
        }

        private void UserDataChanged()
        {
            UserDto user = UserService.GetByUsername(currentUserName);
            if (user != null)
            {
                CurrentUserAccount = new UserAccountEntity(user.Username, $"Welcome" +
                $"{(!string.IsNullOrEmpty(user.FirstName) ? " " + user.FirstName : "")}" +
                        $"{(!string.IsNullOrEmpty(user.LastName) ? " " + user.LastName : "")}");
            }
            else { MessageBoxViewModel.Show("Invalid user, not logged in"); }
        }

        private async void CheckIfAlerted()
        {
            while (true)
            {
                UserDto user = UserService.GetByUsername(currentUserName);
                currentUserName = Thread.CurrentPrincipal.Identity.Name;

                List<ThingDto> things = UserService.GetAllThingsByUser(user.Id);
                foreach (var thing in things)
                {
                    if (thing.IsAlerted)
                    {
                        MessageBoxViewModel.Show(AreaService.GetAreaById(thing.AreaId).Name + " " +
                            thing.Name.ToLower() + " is alerted.");
                        ThingService.UpdateThing(thing.Id, ThingMapper.MapToDto(new ThingModel(
                            thing.Id, thing.Name, thing.Ip, thing.IsVideo, false, thing.Description, thing.AreaId,
                            AreaMapper.MapToModel(AreaService.GetAreaById(thing.AreaId)))));
                    }
                }
                await Task.Delay(5000);
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
            CurrentChildView = new SettingViewModel();
            Caption = "Settings Page";
            Icon = IconChar.Gear;
        }
    }
}
