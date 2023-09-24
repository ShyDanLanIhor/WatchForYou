﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories.IRepositories;
using System.Security;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System.ComponentModel;
using Shyryi_WatchForYou.Services.ModelServices;
using System.Windows;

namespace Shyryi_WatchForYou.Commands.EnterViewModel
{
    public class LoginCommand : CommandBase
    {
        private readonly SignInViewModel _signInViewModel;

        private AriaService userService;
        public LoginCommand(SignInViewModel signInViewModel)
        {

            _signInViewModel = signInViewModel;

            userService = new AriaService();

            _signInViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignInViewModel.Username) ||
                e.PropertyName == nameof(SignInViewModel.Password))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity(_signInViewModel.Username), null);
            ExecuteLogInAsync("* Invalid username or password");
        }

        public override bool CanExecute(object parameter)
        {
            bool validData = string.IsNullOrWhiteSpace(
                _signInViewModel.Username) ||
                _signInViewModel.Username.Length < 3 || 
                _signInViewModel.Password == null ||
                _signInViewModel.Password.Length < 3 ? false : true;
            return validData;
        }

        private async void ExecuteLogInAsync(string errorMessage)
        {
            if (userService.AuthenticateUser(new System.Net.NetworkCredential(
                _signInViewModel.Username, _signInViewModel.Password)))
            {
                App.Current.MainWindow.Visibility = Visibility.Collapsed;
            }
            else
            {
                _signInViewModel.ErrorMessage = errorMessage;
                await Task.Delay(2000);
                _signInViewModel.ErrorMessage = "";
            }
        }
    }
}