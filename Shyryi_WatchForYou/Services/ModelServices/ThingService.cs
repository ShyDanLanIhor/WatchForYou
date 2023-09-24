using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public class ThingService
    {
        private readonly AreaRepository areaRepository = new AreaRepository();
        private readonly ThingRepository thingRepository = new ThingRepository();

        public List<ThingDto> GetThingsByArea(int areaId)
        {
            return thingRepository.GetThingsByArea(areaId);
        }

        public ThingDto GetThingById(int thingId)
        {
            return thingRepository.GetThingById(thingId);
        }

        public bool CreateThing(ThingDto thing)
        {
            return thingRepository.AddThing(thing);
        }

        public bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            return thingRepository.UpdateThing(thingId, updatedThing);
        }

        public bool RemoveThing(int thingId)
        {
            return thingRepository.RemoveThingById(thingId);
        }
    }
}
