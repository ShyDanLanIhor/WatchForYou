using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shyryi_WatchForYou.Repositories
{
    public static class AreaRepository
    {

        public static List<AreaDto> GetAreasByUser(string username)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return (from a in DbContextService.DbContext.Area
                        where a.User.Username == username
                        select a).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch areas for the user!");
                return new List<AreaDto>();
            }
        }

        public static bool AddArea(AreaDto area)
        {
            DbContextService.DbContext = new WatchForYouContext();
            var user = DbContextService.DbContext.User.SingleOrDefault(u => u.Username == area.User.Username);
            if (user != null)
            {
                area.User = user;
                DbContextService.DbContext.Area.Add(area);
                DbContextService.DbContext.SaveChanges();
                return true;
            }
            else
            {
                MessageBox.Show("User not found!");
                return false;
            }
        }

        public static bool RemoveAreaById(int areaId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                var area = DbContextService.DbContext.Area.Find(areaId);
                if (area != null)
                {
                    DbContextService.DbContext.Area.Remove(area);
                    DbContextService.DbContext.SaveChanges();
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
        public static AreaDto GetAreaById(int areaId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return DbContextService.DbContext.Area.FirstOrDefault(a => a.Id == areaId);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch area from the database!");
                return null;
            }
        }

        public static bool UpdateArea(AreaDto area)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                DbContextService.DbContext.Area.Update(area);
                DbContextService.DbContext.SaveChanges();
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
