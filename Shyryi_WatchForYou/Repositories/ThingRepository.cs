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
    public class ThingRepository : RepositoryBase
    {
        private readonly WatchForYouContext _dbContext = new WatchForYouContext();

        public List<ThingDto> GetThingsByArea(int areaId)
        {
            try
            {
                return _dbContext.Thing.Where(t => t.Area.Id == areaId).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch things for the area!");
                return new List<ThingDto>();
            }
        }

        public ThingDto GetThingById(int thingId)
        {
            try
            {
                return _dbContext.Thing.FirstOrDefault(t => t.Id == thingId);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch thing by ID!");
                return null;
            }
        }

        public bool AddThing(ThingDto thing)
        {
            var area = _dbContext.Area.SingleOrDefault(a => a.Id == thing.Area.Id);
            if (area != null)
            {
                thing.Area = area;
                _dbContext.Thing.Add(thing);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                MessageBox.Show("Area not found!");
                return false;
            }
        }

        public bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            try
            {
                var existingThing = _dbContext.Thing.FirstOrDefault(t => t.Id == thingId);
                if (existingThing != null)
                {
                    existingThing.Name = updatedThing.Name;
                    existingThing.Ip = updatedThing.Ip;
                    existingThing.IsVideo = updatedThing.IsVideo;
                    existingThing.IsAlerted = updatedThing.IsAlerted;
                    existingThing.Description = updatedThing.Description;
                    existingThing.Area = updatedThing.Area;

                    _dbContext.Thing.Update(existingThing);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBox.Show("Thing not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot update thing!");
                return false;
            }
        }

        public bool RemoveThingById(int thingId)
        {
            try
            {
                var thing = _dbContext.Thing.Find(thingId);
                if (thing != null)
                {
                    _dbContext.Thing.Remove(thing);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBox.Show("Thing not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot remove thing from the database!");
                return false;
            }
        }
    }
}
