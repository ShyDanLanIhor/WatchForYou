using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.ViewModels;

namespace Shyryi_WatchForYou.Models.Repositories
{
    public static class UserRepository
    {
        public static bool AddUser(UserDto user)
        {
            DbContextService.DbContext = new WatchForYouContext();
            DbContextService.DbContext.User.Add(user);
            DbContextService.DbContext.SaveChanges();
            return true;
        }

        public static UserDto GetByUsername(string username)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return (from u in DbContextService.DbContext.User
                        where u.Username == username
                        select u).FirstOrDefault();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch user from the database!");
                return null;
            }
        }

        public static UserDto GetByEmail(string email)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return (from u in DbContextService.DbContext.User
                        where u.Email == email
                        select u).FirstOrDefault();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch user from the database!");
                return null;
            }
        }

        public static List<UserDto> GetUsersByUsernameAndPassword(string username, string password)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return (from u in DbContextService.DbContext.User
                        where u.Username == username && u.Password == password
                        select u).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the database!");
                return new List<UserDto>();
            }
        }

        public static bool RemoveUserByUsername(string username)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                var user = (from u in DbContextService.DbContext.User
                            where u.Username == username
                            select u).FirstOrDefault();
                if (user != null)
                {
                    DbContextService.DbContext.User.Remove(user);
                    DbContextService.DbContext.SaveChanges();
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

        public static UserDto GetById(int userId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return DbContextService.DbContext.User.FirstOrDefault(u => u.Id == userId);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch user from the database!");
                return null;
            }
        }

        public static bool UpdateUser(UserDto user)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                DbContextService.DbContext.User.Update(user);
                DbContextService.DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot update user in the database!");
                return false;
            }
        }

        public static List<ThingDto> GetAllThingsByUser(int userId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                List<ThingDto> wholeList = new List<ThingDto>();
                UserDto user = GetById(userId);
                foreach (var area in AreaRepository.GetAreasByUser(user.Username))
                {
                    foreach (var thing in ThingRepository.GetThingsByArea(area.Id))
                    {
                        wholeList.Add(thing);
                    }
                }
                return wholeList;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch things for the user!");
                return new List<ThingDto>();
            }
        }
    }
}

