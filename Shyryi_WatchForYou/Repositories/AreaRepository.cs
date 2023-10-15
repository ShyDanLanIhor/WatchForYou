using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Shyryi_WatchForYou.Repositories
{
    public static class AreaRepository
    {
        private static readonly object areaRepositoryLock = new object();
        public static List<AreaDto> GetAreasByUser(string username)
        {
            lock (areaRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Get areas by current user",
                        Data = new
                        {
                            Username = username
                        }
                    }));
                return TcpClientService.Read<List<AreaDto>>();
            }
        }

        public static bool AddArea(AreaDto area)
        {
            lock (areaRepositoryLock)
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
        }

        public static bool RemoveAreaById(int areaId)
        {
            lock (areaRepositoryLock)
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
        }

        public static bool UpdateArea(int areaId, AreaDto updatedArea)
        {
            lock (areaRepositoryLock)
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
        }
        public static AreaDto GetAreaById(int areaId)
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
        public static bool CheckIfAreaExist(string areaName, string userName)
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
    }
}
