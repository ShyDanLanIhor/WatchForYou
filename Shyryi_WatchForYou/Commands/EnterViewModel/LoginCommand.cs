using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Shyryi_WatchForYou.Models.Repositories;
using System.Security;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System.ComponentModel;
using Shyryi_WatchForYou.Services.ModelServices;
using System.Windows;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Exceptions;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace Shyryi_WatchForYou.Commands.EnterViewModel
{
    public class LoginCommand : CommandBase
    {
        private readonly SignInViewModel _signInViewModel;

        public LoginCommand(SignInViewModel signInViewModel)
        {

            _signInViewModel = signInViewModel;

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
            ExecuteLogInAsync();
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

        private async void ExecuteLogInAsync()
        {
            try
            {
                if (UserService.AuthenticateUser(new System.Net.NetworkCredential(
                    _signInViewModel.Username, _signInViewModel.Password)))
                {
                    App.Current.MainWindow.Visibility = Visibility.Collapsed;
                }
                else
                {
                    throw new ExistingDataException("* Invalid username or password");
                }
            }
            catch (InputDataException e)
            {
                _signInViewModel.ErrorMessage = e.Message;
                await Task.Delay(2000);
                _signInViewModel.ErrorMessage = "";
            }
            catch (Exception)
            {
                _signInViewModel.ErrorMessage = "Can not connect to database!";
                await Task.Delay(2000);
                _signInViewModel.ErrorMessage = "";
            }
        }
    }
}
