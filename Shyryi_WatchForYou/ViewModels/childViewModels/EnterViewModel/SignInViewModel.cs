using FontAwesome.Sharp;
using InfinityStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Commands.EnterViewModel;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories.IRepositories;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Stores;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.DTOs;

namespace Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel
{
    public class SignInViewModel : ViewModelBase

    {
        // fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private UserService userService = new UserService();

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
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand GoToSigningUpCommand { get; }
        public SignInViewModel(NavigationService signUpViewNavigationService)
        {
            LoginCommand = new LoginCommand(this);

            GoToSigningUpCommand = new NavigateCommand(signUpViewNavigationService);
        }

    }
}
