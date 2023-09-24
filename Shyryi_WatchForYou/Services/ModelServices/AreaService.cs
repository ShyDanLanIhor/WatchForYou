using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public class AreaService
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly AreaRepository areaRepository = new AreaRepository();

        public List<AreaDto> GetAreasByCurrentUser()
        {
            UserDto user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            List<AreaDto> values = areaRepository.GetAreasByUser(user.Username);
            
            foreach (AreaDto value in values)
            {
                value.User = user;
            }
            return values;
        }

        public bool CreateArea(AreaDto area)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            area.User = new UserDto { Username = username };
            return areaRepository.AddArea(area);
        }

        public bool RemoveArea(int areaId)
        {
            return areaRepository.RemoveAreaById(areaId);
        }
        public bool UpdateArea(int areaId, AreaDto updatedArea)
        {
            try
            {
                var existingArea = areaRepository.GetAreaById(areaId);
                if (existingArea != null)
                {
                    existingArea.Name = updatedArea.Name;
                    existingArea.Description = updatedArea.Description;

                    areaRepository.UpdateArea(existingArea);
                    return true;
                }
                else
                {
                    MessageBox.Show("Area not found!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot update area!");
                return false;
            }
        }
        public AreaDto GetAreaById(int areaId)
        {
            try
            {
                return areaRepository.GetAreaById(areaId);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot fetch area by ID!");
                return null;
            }
        }
    }
}
