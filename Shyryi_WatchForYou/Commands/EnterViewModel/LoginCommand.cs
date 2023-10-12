using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System.ComponentModel;
using Shyryi_WatchForYou.Services.ModelServices;
using System.Windows;
using Shyryi_WatchForYou.Exceptions;
using System.Windows.Media;
using Shyryi_WatchForYou.Services;
using System.Net.Sockets;
using System.IO;

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
            }
            catch (TypeInitializationException)
            {
                _signInViewModel.SingInInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                _signInViewModel.SignInInfo = "Cannot connect to the server!";
                await Task.Delay(2000);
                _signInViewModel.SignInInfo = "";
            }
            catch (IOException)
            {
                _signInViewModel.SingInInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                _signInViewModel.SignInInfo = "Cannot connect to the server!";
                await Task.Delay(2000);
                App.Current.Dispatcher.Invoke(() =>
                { App.Current.Shutdown(); });
                _signInViewModel.SignInInfo = "";
            }
            catch (InputDataException e)
            {
                _signInViewModel.SingInInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                _signInViewModel.SignInInfo = e.Message;
                await Task.Delay(2000);
                _signInViewModel.SignInInfo = "";
            }
            catch (Exception)
            {
                _signInViewModel.SingInInfoColor = (Brush)App.Current.FindResource("ErrorMessageColor");
                _signInViewModel.SignInInfo = "User was not logged in!";
                await Task.Delay(2000);
                _signInViewModel.SignInInfo = "";
            }
        }
    }
}
