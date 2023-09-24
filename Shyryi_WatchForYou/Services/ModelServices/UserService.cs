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
    public class AriaService
    {
        private readonly UserRepository 
            userRepository = new UserRepository();

        public bool CreateAccount(UserDto user)
        {
            return userRepository.AddUser(user);
        }
        public UserDto GetBy(string username)
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
    }
}