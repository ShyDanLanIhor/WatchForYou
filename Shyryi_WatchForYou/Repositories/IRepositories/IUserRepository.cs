using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models;

namespace Shyryi_WatchForYou.Repositories.IRepositories
{
    public interface IUserRepository
    {
        public bool AddUser(UserDto user);
        public UserDto GetByUsername(string username);
        public List<UserDto> GetUsersByUsernameAndPassword(string username, string password);
        public bool RemoveUserByUsername(string username);
    }
}
