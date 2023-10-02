using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories;
using System.Collections.Generic;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class ThingService
    {
        public static List<ThingDto> GetThingsByArea(int areaId)
        {
            return ThingRepository.GetThingsByArea(areaId);
        }

        public static ThingDto GetThingById(int thingId)
        {
            return ThingRepository.GetThingById(thingId);
        }

        public static bool CreateThing(ThingDto thing)
        {
            return ThingRepository.AddThing(thing);
        }

        public static bool UpdateThing(int thingId, ThingDto updatedThing)
        {
            return ThingRepository.UpdateThing(thingId, updatedThing);
        }

        public static bool RemoveThing(int thingId)
        {
            return ThingRepository.RemoveThingById(thingId);
        }
        public static bool SetThingIsAlerted(int thingId, bool isAlerted)
        {
            return ThingRepository.SetThingIsAlerted(thingId, isAlerted);
        }
    }
}

