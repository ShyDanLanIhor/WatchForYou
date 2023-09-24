using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Commands.EnterViewModel
{
    public class RegisterCommand : CommandBase
    {
        private readonly SignUpViewModel _signUpViewModel;

        private UserService userService;
        public RegisterCommand(SignUpViewModel signUpViewModel)
        {

            _signUpViewModel = signUpViewModel;

            userService = new UserService();

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
            userService.CreateAccount(UserMapper
                .MapToDto(new UserModel(
                    _signUpViewModel.Username,
                    new NetworkCredential(string.Empty, _signUpViewModel.Password).Password,
                    _signUpViewModel.Email)));
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
