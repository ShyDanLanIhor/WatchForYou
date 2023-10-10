using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class AreaService
    {
        public static List<AreaDto> GetAreasByCurrentUser()
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
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch area!");
                return null;
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
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch area by ID!");
                return false;
            }
        }
    }
}

