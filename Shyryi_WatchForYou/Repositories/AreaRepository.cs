using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shyryi_WatchForYou.Repositories
{
    public class AreaRepository : RepositoryBase
    {
        private readonly WatchForYouContext _dbContext = new WatchForYouContext();

        public List<AreaDto> GetAreasByUser(string username)
        {
            try
            {
                return (from a in _dbContext.Area
                        where a.User.Username == username
                        select a).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch areas for the user!");
                return new List<AreaDto>();
            }
        }

        public bool AddArea(AreaDto area)
        {
            var user = _dbContext.User.SingleOrDefault(u => u.Username == area.User.Username);
            if (user != null)
            {
                area.User = user;
                _dbContext.Area.Add(area);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                MessageBox.Show("User not found!");
                return false;
            }
        }

        public bool RemoveAreaById(int areaId)
        {
            try
            {
                var area = _dbContext.Area.Find(areaId);
                if (area != null)
                {
                    _dbContext.Area.Remove(area);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBox.Show("Area not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot remove area from the database!");
                return false;
            }
        }
        public AreaDto GetAreaById(int areaId)
        {
            try
            {
                return _dbContext.Area.FirstOrDefault(a => a.Id == areaId);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch area from the database!");
                return null;
            }
        }

        public bool UpdateArea(AreaDto area)
        {
            try
            {
                _dbContext.Area.Update(area);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot update area in the database!");
                return false;
            }
        }
    }
}
