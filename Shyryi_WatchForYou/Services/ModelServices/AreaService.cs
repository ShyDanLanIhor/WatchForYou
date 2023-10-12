using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class AreaService
    {
        public static List<AreaDto> GetAreasByCurrentUser()
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Get areas by current user",
                        Data = new
                        {
                            Username = Thread.CurrentPrincipal.Identity.Name
                        }
                    }));
                return TcpClientService.Read<List<AreaDto>>();
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<List<AreaDto>>(ex);
            }
        }

        public static bool CreateArea(AreaDto area)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Create area",
                        Data = JsonConvert.SerializeObject(
                            new
                            {
                                Username = Thread.CurrentPrincipal.Identity.Name,
                                Area = area
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

        public static bool RemoveArea(int areaId)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Remove area",
                        Data = new
                        {
                            AreaId = areaId
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

        public static bool UpdateArea(int areaId, AreaDto updatedArea)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Update area",
                        Data = JsonConvert.SerializeObject(
                            new
                            {
                                AreaId = areaId,
                                UpdatedArea = updatedArea
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

        public static AreaDto GetAreaById(int areaId)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Get area by id",
                        Data = new
                        {
                            AreaId = areaId
                        }
                    }));
                return TcpClientService.Read<AreaDto>();
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<AreaDto>(ex);
            }
        }
        public static bool CheckIfAreaExist(string areaName, string userName)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Check if area exist",
                        Data = new
                        {
                            AreaName = areaName,
                            UserName = userName
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
    }
}

