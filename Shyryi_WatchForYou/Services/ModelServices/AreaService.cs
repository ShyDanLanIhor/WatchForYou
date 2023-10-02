using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories;
using Shyryi_WatchForYou.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class AreaService
    {
        public static List<AreaDto> GetAreasByCurrentUser()
        {
            UserDto user = UserRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            List<AreaDto> values = AreaRepository.GetAreasByUser(user.Username);

            foreach (AreaDto value in values)
            {
                value.User = user;
            }
            return values;
        }

        public static bool CreateArea(AreaDto area)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            area.User = new UserDto { Username = username };
            return AreaRepository.AddArea(area);
        }

        public static bool RemoveArea(int areaId)
        {
            return AreaRepository.RemoveAreaById(areaId);
        }

        public static bool UpdateArea(int areaId, AreaDto updatedArea)
        {
            try
            {
                var existingArea = AreaRepository.GetAreaById(areaId);
                if (existingArea != null)
                {
                    existingArea.Name = updatedArea.Name;
                    existingArea.Description = updatedArea.Description;

                    AreaRepository.UpdateArea(existingArea);
                    return true;
                }
                else
                {
                    MessageBoxViewModel.Show("Area not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot update area!");
                return false;
            }
        }

        public static AreaDto GetAreaById(int areaId)
        {
            try
            {
                AreaDto area = AreaRepository.GetAreaById(areaId);
                area.User = UserRepository.GetById(area.UserId);
                return area;
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch area by Name!");
                return null;
            }
        }
        public static bool CheckIfAreaExist(string areaName, string userName)
        {
            try
            {
                bool check = false;
                List<AreaDto> areas = AreaRepository.GetAreasByUser(userName);
                foreach(var area in areas)
                {
                    if (area.Name == areaName) { check = true; }
                }
                return AreaRepository.GetAreaByName(areaName) != null && check;
            }
            catch (Exception)
            {
                MessageBoxViewModel.Show("Cannot fetch area by ID!");
                return false;
            }
        }
    }
}

