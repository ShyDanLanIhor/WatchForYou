using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using Shyryi_WatchForYou_Server.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class UserService
    {
        public static bool CreateAccount(UserDto user)
        {
            if (Regex.IsMatch(user.Email, @"^$|^.*@.*\..*$") != true)
            { throw new InvalidDataInputException("Invalid email input!"); }
            if (Regex.IsMatch(user.Username, @"^[a-zA-Z0-9]*(?<!\.)(?<!@)$") != true)
            { throw new InvalidDataInputException("Invalid username input!"); }
            if (UserRepository.GetByUsername(user.Username) != null)
            { throw new ExistingDataException("Username already exist!"); }
            if (UserRepository.GetByEmail(user.Email) != null)
            { throw new ExistingDataException("Email already connected!"); }
            return UserRepository.AddUser(user);
        }
        public static UserDto GetByUsername(string username)
        {
            try
            {
                return UserRepository.GetByUsername(username);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<UserDto>(ex);
            }
        }
        public static UserDto GetByEmail(string email)
        {
            try
            {
                return GetByEmail(email);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<UserDto>(ex);
            }
        }

        public static bool AuthenticateUser(NetworkCredential credential)
        {
            UserDto user = UserRepository.GetUsersByUsernameAndPassword(credential);
            if (user == null) { throw new InvalidDataInputException("Invalid username or password"); }
            if (user.IsVerificated == false) { throw new InvalidDataInputException("Email is not verificated"); }
            return true;
        }

        public static bool DeleteAccount(string username)
        {
            try
            {
                return UserRepository.RemoveUserByUsername(username);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }

        public static bool UpdateUser(int userId, UserDto updatedUser)
        {
            UserDto currentUser = UserRepository
                .GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (UserRepository.GetByUsername(updatedUser.Username) != null 
                && updatedUser.Username != currentUser.Username)
                throw new ExistingDataException("Username already exist!");
            if (UserRepository.GetByEmail(updatedUser.Email) != null
                && updatedUser.Email != currentUser.Email)
                throw new ExistingDataException("Email already connected!");
            if (!Regex.IsMatch(updatedUser.Email, @"^$|^.*@.*\..*$"))
                throw new InvalidDataInputException("Invalid email input!");
            if (!Regex.IsMatch(updatedUser.Username, @"^[a-zA-Z0-9_.-]*(?<!\.)(?<!@)$"))
                throw new InvalidDataInputException("Invalid username input!");
            updatedUser.IsVerificated = currentUser.IsVerificated;
            updatedUser.ConfirmationToken = currentUser.ConfirmationToken;
            return UserRepository.UpdateUser(userId, updatedUser);
        }

        public static bool UpdateUserPassword(string username)
        {
            string newPassword = EmailService.GenerateUniqueToken(10);
            UserDto user = UserRepository.GetByUsername(username);
            if (user == null) { throw new InvalidDataInputException("User does not exist!"); }
            if (user.IsVerificated == false) { throw new InvalidDataInputException("Email is not verificated"); }
            EmailService.SendPasswordOnEmail(user.Email, newPassword);
            return UserRepository.UpdateUserPassword(user, newPassword);
        }
        public static List<ThingDto> GetAllThingsByUser(int userId)
        {
            try
            {
                return UserRepository.GetAllThingsByUser(userId);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<List<ThingDto>>(ex);
            }
        }
    }
}