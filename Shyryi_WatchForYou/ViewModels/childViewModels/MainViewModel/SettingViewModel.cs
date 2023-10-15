using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services.ModelServices;
using System;
using System.Net;
using System.Security;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using Shyryi_WatchForYou.Exceptions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Shyryi_WatchForYou.Services;
using System.Security.Principal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        private string _settingsInfo;
        private Brush _settingsInfoColor;
        private UserDto currentUser;

        public static event Action<string> UserDataChanged;

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
        public string SettingsInfo
        {
            get => _settingsInfo;
            set
            {
                _settingsInfo = value;
                OnPropertyChanged(nameof(SettingsInfo));
            }
        }
        public Brush SettingsInfoColor
        {
            get => _settingsInfoColor;
            set
            {
                _settingsInfoColor = value;
                OnPropertyChanged(nameof(SettingsInfoColor));
            }
        }

        public ICommand ChangeCommand { get; }
        public SettingViewModel()
        {
            currentUser = UserService.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            Username = currentUser.Username;
            Email = currentUser.Email;
            FirstName = currentUser.FirstName;
            LastName = currentUser.LastName;

            ChangeCommand = new RelayCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }

        private void ExecuteChangeCommand(object obj)
        {
            try
            {
                UserService.UpdateUser(currentUser.Id, UserMapper.MapToDto(
                    new UserModel( currentUser.Id, Username,
                        new NetworkCredential(string.Empty, NewPassword).Password,
                        FirstName, LastName, Email)));
                UserDataChanged?.Invoke(Username);
                Task.Run(async () =>
                {
                    SettingsInfoColor = (Brush)App.Current
                    .FindResource("SettingsColor");
                    SettingsInfo = "User data was changed!";
                    await Task.Delay(2000);
                    SettingsInfo = "";
                });
            }
            catch (Exception ex)
            {
                Task.Run(async () =>
                {
                    (SettingsInfoColor, SettingsInfo) =
                        ExceptionService.HandleGUIException(ex);
                    await Task.Delay(2000);
                    SettingsInfo = "";
                });
            }
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
