using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories;
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
                List<AreaDto> areas = AreaRepository.GetAreasByUser(
                    Thread.CurrentPrincipal.Identity.Name);
                if (areas == null) { throw new 
                        NullReturnDataExeption("No areas was found!"); }
                return areas;
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<List<AreaDto>>(ex);
            }
        }

        public static bool CreateArea(AreaDto area)
        {
            if (AreaRepository.CheckIfAreaExist(area.Name, Thread.CurrentPrincipal.Identity.Name))
            { throw new ExistingDataException("Area name already exist!"); }
            area.User = UserRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            area.UserId = area.User.Id;
            return AreaRepository.AddArea(area);
        }

        public static bool RemoveArea(int areaId)
        {
            try
            {
                return AreaRepository.RemoveAreaById(areaId);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }

        public static bool UpdateArea(int areaId, AreaDto updatedArea)
        {
            if (AreaRepository.CheckIfAreaExist(updatedArea.Name, Thread.CurrentPrincipal.Identity.Name) 
                && updatedArea.Name != AreaRepository.GetAreaById(areaId).Name)
            { throw new ExistingDataException("Area name already exist!"); }
            updatedArea.Id = areaId;
            updatedArea.User = UserRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            updatedArea.UserId = updatedArea.User.Id;
            return AreaRepository.UpdateArea(areaId, updatedArea);
        }

        public static AreaDto GetAreaById(int areaId)
        {
            try
            {
                return AreaRepository.GetAreaById(areaId);
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
                return AreaRepository.CheckIfAreaExist(areaName, userName);
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }
    }
}

