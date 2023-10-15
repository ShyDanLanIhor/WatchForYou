using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Repositories;
using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class ThingService
    {
        public static List<ThingDto> GetThingsByArea(int areaId)
        {
            try
            {
                return ThingRepository.GetThingsByArea(areaId);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<List<ThingDto>>(ex);
            }
        }

        public static ThingDto GetThingById(int thingId)
        {
            try
            {
                return ThingRepository.GetThingById(thingId);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<ThingDto>(ex);
            }
        }

        public static bool CreateThing(ThingDto thing)
        {
            if (Regex.IsMatch(thing.Ip, @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}:(25[0-5]|(2[0-4]|1\d|[1-9]|)\d){2}$") != true &&
                Regex.IsMatch(thing.Ip, @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$") != true)
            { throw new InvalidDataInputException("Invalid device IP input!"); }
            thing.Area = AreaRepository.GetAreaById(thing.AreaId);
            return ThingRepository.AddThing(thing);
        }

        public static bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            if (Regex.IsMatch(updatedThing.Ip, @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}:(25[0-5]|(2[0-4]|1\d|[1-9]|)\d){2}$") != true &&
                Regex.IsMatch(updatedThing.Ip, @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$") != true)
            { throw new InvalidDataInputException("Invalid device IP input!"); }
            updatedThing.Area = AreaRepository.GetAreaById(updatedThing.AreaId);
            return ThingRepository.UpdateThing(thingId, updatedThing);
        }

        public static bool RemoveThing(int thingId)
        {
            try
            {
                return ThingRepository.RemoveThingById(thingId);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }
        public static bool SetThingIsAlerted(int thingId, bool isAlerted)
        {
            try
            {
                return ThingRepository.SetThingIsAlerted(thingId, isAlerted);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }
    }
}

