using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Linq;

namespace Shyryi_WatchForYou.Models
{
    public class AreaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public ICollection<ThingModel> Things { get; set; }

        public AreaModel(string name, string description)
        {
            Name = name;
            Description = description;
            UserId = 0;
            this.User = new UserModel();
            Things = new List<ThingModel>();
        }

        public AreaModel(int id, string name, string description, int userId, UserModel user)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
            User = user;
            Things = new List<ThingModel>();
        }

        public AreaModel(AreaModel other)
        {
            Id = other.Id;
            Name = other.Name;
            Description = other.Description;
            UserId = other.UserId;
            User = new UserModel(other.User);
            Things = other.Things.Select(thing => new ThingModel(thing)).ToList();
        }
    }
}
