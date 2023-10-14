using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Shyryi_WatchForYou.Commands.EnterViewModel
{
    public class RegisterCommand : CommandBase
    {
        private readonly SignUpViewModel _signUpViewModel;

        public RegisterCommand(SignUpViewModel signUpViewModel)
        {

            _signUpViewModel = signUpViewModel;

            _signUpViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignUpViewModel.Username) ||
                e.PropertyName == nameof(SignUpViewModel.Password) ||
                e.PropertyName == nameof(SignUpViewModel.Email))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            ExecuteRegisterAsync();
        }
        private async void ExecuteRegisterAsync()
        {
            try
            {
                if(UserService.GetByUsername(_signUpViewModel.Username) != null)
                { throw new ExistingDataException("Username already exist!"); }
                if (UserService.GetByEmail(_signUpViewModel.Email) != null)
                { throw new ExistingDataException("Email already connected!"); }
                if (Regex.IsMatch(_signUpViewModel.Email, @"^$|^.*@.*\..*$") != true)
                { throw new InvalidDataInputException("Invalid email input!"); }
                if (Regex.IsMatch(_signUpViewModel.Username, @"^[a-zA-Z0-9]*(?<!\.)(?<!@)$") != true)
                { throw new InvalidDataInputException("Invalid username input!"); }
                string confirmationToken = EmailService.GenerateUniqueToken(32);
                string confirmationLink = $"https://localhost:7062/User/Confirm?token={confirmationToken}";
                EmailService.SendConfirmationEmail(_signUpViewModel.Email, confirmationLink);
                UserService.CreateAccount(UserMapper
                    .MapToDto(new UserModel(
                        _signUpViewModel.Username,
                        new NetworkCredential(string.Empty, _signUpViewModel.Password).Password,
                        _signUpViewModel.Email, false, confirmationToken)));
                _signUpViewModel.SingUpInfoColor = (Brush)App.Current.FindResource("SignUpColor");
                _signUpViewModel.SignUpInfo = "Account was created!";
                await Task.Delay(2000);
                _signUpViewModel.SignUpInfo = "";
            }
            catch (Exception ex)
            {
                (_signUpViewModel.SingUpInfoColor, _signUpViewModel.SignUpInfo) =
                    ExceptionService.HandleGUIException(ex);
                await Task.Delay(2000);
                _signUpViewModel.SignUpInfo = "";
            }
        }

        public override bool CanExecute(object parameter)
        {
            bool validData = string.IsNullOrWhiteSpace(
                _signUpViewModel.Username) ||
                _signUpViewModel.Username.Length < 3 ||
                _signUpViewModel.Password == null ||
                _signUpViewModel.Password.Length < 3 ||
                _signUpViewModel.Email == null ||
                _signUpViewModel.Email.Length < 3 ? false : true;
            return validData;
        }
    }
}
