using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Services
{
    public class AreaService
    {
        private readonly AreaRepository areaRepository = new AreaRepository();

        public List<AreaDto> GetAreasByCurrentUser()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            return areaRepository.GetAreasByUser(username);
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
    }
}
