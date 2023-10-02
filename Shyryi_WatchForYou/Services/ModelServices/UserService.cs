using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.ViewModels;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class UserService
    {
        public static bool CreateAccount(UserDto user)
        {
            return UserRepository.AddUser(user);
        }

        public static UserDto GetByUsername(string username)
        {
            try
            {
                return UserRepository.GetByUsername(username);
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch user!");
                return null;
            }
        }
        public static UserDto GetByEmail(string email)
        {
            try
            {
                return UserRepository.GetByEmail(email);
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch user!");
                return null;
            }
        }

        public static bool AuthenticateUser(NetworkCredential credential)
        {
            UserDto user = UserRepository.GetUsersByUsernameAndPassword(credential.UserName, credential.Password);
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
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot remove user!");
                return false;
            }
        }

        public static bool UpdateUser(int userId, UserDto updatedUser)
        {
            try
            {
                var existingUser = UserRepository.GetById(userId);
                if (existingUser != null)
                {
                    existingUser.Username = updatedUser.Username;
                    existingUser.Password = updatedUser.Password;
                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.LastName = updatedUser.LastName;
                    existingUser.Email = updatedUser.Email;
                    existingUser.IsVerificated = updatedUser.IsVerificated;
                    existingUser.ConfirmationToken = updatedUser.ConfirmationToken;

                    if (updatedUser.Areas != null)
                    {
                        existingUser.Areas = updatedUser.Areas;
                    }

                    UserRepository.UpdateUser(existingUser);
                    return true;
                }
                else
                {
                    MessageBoxViewModel.Show("User not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot update user!");
                return false;
            }
        }

        public static bool UpdateUserPassword(UserDto updatedUser, string password)
        {
            try
            {
                updatedUser.Password = password;
                UserRepository.UpdateUser(updatedUser);
                return true;
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot update user!");
                return false;
            }
        }

        public static List<ThingDto> GetAllThingsByUser(int userId)
        {
            try
            {
                return UserRepository.GetAllThingsByUser(userId);
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch things for the user!");
                return new List<ThingDto>();
            }
        }
    }

}