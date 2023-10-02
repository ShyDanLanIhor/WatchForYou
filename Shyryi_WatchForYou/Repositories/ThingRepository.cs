﻿using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shyryi_WatchForYou.Repositories
{
    public static class ThingRepository
    {
        public static List<ThingDto> GetThingsByArea(int areaId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return DbContextService.DbContext.Thing.Where(t => t.Area.Id == areaId).ToList();
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch things for the area!");
                return new List<ThingDto>();
            }
        }

        public static ThingDto GetThingById(int thingId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                return DbContextService.DbContext.Thing.FirstOrDefault(t => t.Id == thingId);
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch thing by ID!");
                return null;
            }
        }

        public static bool AddThing(ThingDto thing)
        {
            DbContextService.DbContext = new WatchForYouContext();
            var area = DbContextService.DbContext.Area.SingleOrDefault(a => a.Id == thing.Area.Id);
            if (area != null)
            {
                thing.Area = area;
                DbContextService.DbContext.Thing.Add(thing);
                DbContextService.DbContext.SaveChanges();
                return true;
            }
            else
            {
                MessageBoxViewModel.Show("Area not found!");
                return false;
            }
        }

        public static bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                var existingThing = DbContextService.DbContext.Thing.FirstOrDefault(t => t.Id == thingId);
                if (existingThing != null)
                {
                    existingThing.Name = updatedThing.Name;
                    existingThing.Ip = updatedThing.Ip;
                    existingThing.IsVideo = updatedThing.IsVideo;
                    existingThing.IsAlerted = updatedThing.IsAlerted;
                    existingThing.Description = updatedThing.Description;
                    existingThing.Area = updatedThing.Area;

                    DbContextService.DbContext.Thing.Update(existingThing);
                    DbContextService.DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBoxViewModel.Show("Thing not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot update thing!");
                return false;
            }
        }

        public static bool RemoveThingById(int thingId)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                var thing = DbContextService.DbContext.Thing.Find(thingId);
                if (thing != null)
                {
                    DbContextService.DbContext.Thing.Remove(thing);
                    DbContextService.DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBoxViewModel.Show("Thing not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot remove thing from the database!");
                return false;
            }
        }
        public static bool SetThingIsAlerted(int thingId, bool isAlerted)
        {
            DbContextService.DbContext = new WatchForYouContext();
            try
            {
                var existingThing = DbContextService.DbContext.Thing.FirstOrDefault(t => t.Id == thingId);
                if (existingThing != null)
                {
                    existingThing.IsAlerted = isAlerted;
                    DbContextService.DbContext.Thing.Update(existingThing);
                    DbContextService.DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBoxViewModel.Show("Thing not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot update IsAlerted for the thing!");
                return false;
            }
        }
    }
}

