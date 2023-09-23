using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories.IRepositories;

namespace Shyryi_WatchForYou.Models.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly WatchForYouContext 
            _dbContext = new WatchForYouContext();

        public bool AddUser(UserDto user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
            return true;
        }

        public UserDto GetByUsername(string username)
        {
            try
            {
                return (from u in _dbContext.User
                       where u.Username == username
                       select u).FirstOrDefault();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch user from the database!");
                return null;
            }
        }

        public List<UserDto> GetUsersByUsernameAndPassword(string username, string password)
        {
            try
            {
                return (from u in _dbContext.User
                        where u.Username == username && u.Password == password
                        select u).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the database!");
                return new List<UserDto>();
            }
        }

        public bool RemoveUserByUsername(string username)
        {
            try
            {
                var user = (from u in _dbContext.User
                            where u.Username == username
                            select u).FirstOrDefault();
                if (user != null)
                {
                    _dbContext.User.Remove(user);
                    _dbContext.SaveChanges();
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
                MessageBox.Show("Cannot remove user from the database!");
                return false;
            }
        }

    }
}
