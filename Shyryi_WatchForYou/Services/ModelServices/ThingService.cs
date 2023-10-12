using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class ThingService
    {
        public static List<ThingDto> GetThingsByArea(int areaId)
        {
            try
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
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<ThingDto>(ex);
            }
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
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
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
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
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
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }
    }
}

