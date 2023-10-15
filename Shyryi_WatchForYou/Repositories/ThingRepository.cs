using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shyryi_WatchForYou.Repositories
{
    public static class ThingRepository
    {
        private static readonly object thingRepositoryLock = new object();
        public static List<ThingDto> GetThingsByArea(int areaId)
        {
            lock (thingRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(new RequestEntity
                {
                    Type = "Get things by area",
                    Data = new
                    {
                        AreaId = areaId
                    }
                }));
                return TcpClientService.Read<List<ThingDto>>();
            }
        }

        public static ThingDto GetThingById(int thingId)
        {
            lock (thingRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(new RequestEntity
                {
                    Type = "Get thing by id",
                    Data = new
                    {
                        ThingId = thingId
                    }
                }));
                return TcpClientService.Read<ThingDto>();
            }
        }

        public static bool AddThing(ThingDto thing)
        {
            lock (thingRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Create thing",
                        Data = JsonConvert.SerializeObject(thing)
                    }));
                return TcpClientService.Read<bool>() == true;
            }
        }

        public static bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            lock (thingRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Update thing",
                        Data = JsonConvert.SerializeObject(
                            new
                            {
                                ThingId = thingId,
                                UpdatedThing = updatedThing
                            })
                    }));
                return TcpClientService.Read<bool>() == true;
            }
        }

        public static bool RemoveThingById(int thingId)
        {
            lock (thingRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Remove thing",
                        Data = new
                        {
                            ThingId = thingId
                        }
                    }));
                return TcpClientService.Read<bool>() == true;
            }
        }
        public static bool SetThingIsAlerted(int thingId, bool isAlerted)
        {
            lock (thingRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Set thing is alerted",
                        Data = JsonConvert.SerializeObject(
                            new
                            {
                                ThingId = thingId,
                                IsAlerted = isAlerted.ToString()
                            })
                    }));
                return TcpClientService.Read<bool>() == true;
            }
        }
    }
}

