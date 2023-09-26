using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shyryi_WatchForYou.Commands.EnterViewModel;
using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Stores;
using Shyryi_WatchForYou.Services;
using System.Windows.Media;

namespace Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        // fields
        private string _username;
        private SecureString _password;
        private string _email;
        private string _signUpInfo;
        private Brush _signUpInfoColor;

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
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string SignUpInfo
        {
            get => _signUpInfo;
            set
            {
                _signUpInfo = value;
                OnPropertyChanged(nameof(SignUpInfo));
            }
        }
        public Brush SingUpInfoColor
        {
            get => _signUpInfoColor;
            set
            {
                _signUpInfoColor = value;
                OnPropertyChanged(nameof(SingUpInfoColor));
            }
        }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand GoToSigningInCommand { get; }
        public SignUpViewModel(NavigationService signInViewNavigationService)
        {

            RegisterCommand = new RegisterCommand(this);

            GoToSigningInCommand = new NavigateCommand(signInViewNavigationService);
        }
    }
}
