using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class ThingService
    {
        public static List<ThingDto> GetThingsByArea(int areaId)
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

        public static ThingDto GetThingById(int thingId)
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

        public static bool CreateThing(ThingDto thing)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Create thing",
                        Data = JsonConvert.SerializeObject(thing)
                    }));
                return TcpClientService.Read<bool>() == true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            try
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
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RemoveThing(int thingId)
        {
            try
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
            catch (Exception)
            {
                return false;
            }
        }
        public static bool SetThingIsAlerted(int thingId, bool isAlerted)
        {
            try
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}

