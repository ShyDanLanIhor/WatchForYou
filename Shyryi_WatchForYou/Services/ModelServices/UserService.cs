using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.Data.SqlClient;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories.IRepositories;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public class UserService
    {
        private readonly UserRepository 
            userRepository = new UserRepository();

        public bool CreateAccount(UserDto user)
        {
            return userRepository.AddUser(user);
        }
        public UserDto GetByUsername(string username)
        {
            try
            {
                return userRepository.GetByUsername(username);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch user!");
                return null;
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            try
            {
                var user = userRepository.GetUsersByUsernameAndPassword(credential.UserName, credential.Password).Any();
                if (user)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot authenticate user!");
                return false;
            }
        }
        public bool DeleteAccount(string username)
        {
            try
            {
                return userRepository.RemoveUserByUsername(username);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot remove user!");
                return false;
            }
        }
        public bool UpdateUser(int userId, UserDto updatedUser)
        {
            try
            {
                var existingUser = userRepository.GetBy(userId);
                if (existingUser != null)
                {
                    // Оновлюємо всі поля, крім Id
                    existingUser.Username = updatedUser.Username;
                    existingUser.Password = updatedUser.Password;
                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.LastName = updatedUser.LastName;
                    existingUser.Email = updatedUser.Email;

                    // Якщо необхідно оновити зони (Areas), розгляньте цей код
                    if (updatedUser.Areas != null)
                    {
                        existingUser.Areas = updatedUser.Areas;
                    }

                    userRepository.UpdateUser(existingUser);
                    return true;
                }
                else
                {
                    MessageBox.Show("User not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot update user!");
                return false;
            }
        }
    }
}