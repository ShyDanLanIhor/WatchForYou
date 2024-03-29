﻿using System;
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
                UserService.UpdateUserPassword(Username);
                SingInInfoColor = (Brush)App.Current.FindResource("SignInColor");
                SignInInfo = "New password was send on email!";
                await Task.Delay(2000);
                SignInInfo = "";
            }
            catch (Exception ex)
            {
                (SingInInfoColor, SignInInfo) =
                    ExceptionService.HandleGUIException(ex);
                await Task.Delay(2000);
                SignInInfo = "";
            }
        }
    }
}
