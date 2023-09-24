using Shyryi_WatchForYou.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<AreaModel> Areas { get; set; }

        public UserModel() { }
        public UserModel(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            Areas = new List<AreaModel>();
        }

        public UserModel(int id, string username, string password, string firstName, string lastName, string email)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Areas = new List<AreaModel>();
        }

        public UserModel(UserModel other)
        {
            Id = other.Id;
            Username = other.Username;
            Password = other.Password;
            FirstName = other.FirstName;
            LastName = other.LastName;
            Email = other.Email;
            Areas = other.Areas.Select(area => new AreaModel(area)).ToList();
        }
    }
}
