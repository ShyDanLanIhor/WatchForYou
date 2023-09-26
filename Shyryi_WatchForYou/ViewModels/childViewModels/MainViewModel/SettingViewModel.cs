using InfinityStudio.Models;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading;
using System.Windows.Input;
using Shyryi_WatchForYou.ViewModels;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows;

namespace Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private string _username;
        private string _email;
        private string _firstName;
        private string _lastName;
        private SecureString _previousPassword;
        private SecureString _newPassword;
        private UserDto currentUser;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public SecureString PreviousPassword
        {
            get => _previousPassword;
            set
            {
                _previousPassword = value;
                OnPropertyChanged(nameof(PreviousPassword));
            }
        }
        public SecureString NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public ICommand ChangeCommand { get; }
        ViewModels.MainViewModel mainViewModel;
        public SettingViewModel(ViewModels.MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            currentUser = UserService.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            Username = currentUser.Username;
            Email = currentUser.Email;
            FirstName = currentUser.FirstName;
            LastName = currentUser.LastName;

            ChangeCommand = new RelayCommand(ExecuteChangeCommand, CanExecuteChangeCommand);

        }

        private void ExecuteChangeCommand(object obj)
        {
            UserService.UpdateUser(currentUser.Id, UserMapper.MapToDto
                (new UserModel(currentUser.Id, Username,
                new NetworkCredential(string.Empty, NewPassword).Password,
                FirstName, LastName, Email)));
            this.mainViewModel.CurrentUserAccount = new UserAccountEntity(Username, $"Welcome" +
                        $"{(!string.IsNullOrEmpty(FirstName) ? " " + FirstName : "")}" +
                        $"{(!string.IsNullOrEmpty(LastName) ? " " + LastName : "")}");
        }

        private bool CanExecuteChangeCommand(object obj)
        {
            return 
                string.IsNullOrWhiteSpace(Username) ||
                Username.Length < 3 ||
                string.IsNullOrWhiteSpace(FirstName) ||
                FirstName.Length < 3 ||
                string.IsNullOrWhiteSpace(LastName) ||
                LastName.Length < 3 ||
                NewPassword == null ||
                NewPassword.Length < 3 ||
                PreviousPassword == null ||
                PreviousPassword.Length < 3 ||
                string.IsNullOrWhiteSpace(Username) ||
                Email.Length < 3 ? false : true;
        }
    }
}
