using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Commands.EnterViewModel;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using System.Windows.Media;

namespace Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel
{
    public class SignInViewModel : ViewModelBase

    {
        // fields
        private string _username;
        private SecureString _password;
        private string _signInInfo;
        private Brush _signInInfoColor;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string SignInInfo
        {
            get => _signInInfo;
            set
            {
                _signInInfo = value;
                OnPropertyChanged(nameof(SignInInfo));
            }
        }
        public Brush SingInInfoColor
        {
            get => _signInInfoColor;
            set
            {
                _signInInfoColor = value;
                OnPropertyChanged(nameof(SingInInfoColor));
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand GoToSigningUpCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public SignInViewModel(NavigationService signUpViewNavigationService)
        {
            LoginCommand = new LoginCommand(this);

            GoToSigningUpCommand = new NavigateCommand(signUpViewNavigationService);

            ResetPasswordCommand = new RelayCommand(ExecuteResetPasswordCommand);
        }

        private async void ExecuteResetPasswordCommand(object obj)
        {
            try
            {
                UserDto user = UserService.GetByUsername(Username);
                if (user == null) { throw new InvalidDataInputException("User does not exist!"); }
                if (user.IsVerificated == false) { throw new InvalidDataInputException("Email is not verificated"); }
                string newPassword = EmailService.GenerateUniqueToken(10);
                UserService.UpdateUserPassword(user, newPassword);
                EmailService.SendPasswordOnEmail(user.Email, newPassword);
                SingInInfoColor = (Brush)App.Current.FindResource("SignInColor");
                SignInInfo = "New password was send on email!";
                await Task.Delay(2000);
                SignInInfo = "";
            }
            catch (InputDataException e)
            {
                SingInInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                SignInInfo = e.Message;
                await Task.Delay(2000);
                SignInInfo = "";
            }
            catch (Exception)
            {
                SingInInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                SignInInfo = "Password was not reseted!";
                await Task.Delay(2000);
                SignInInfo = "";
            }
        }
    }
}
