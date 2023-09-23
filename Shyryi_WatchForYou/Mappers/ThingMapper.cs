using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Mappers
{
    public static class ThingMapper
    {
        public static ThingModel MapToModel(ThingDto thingDto)
        {
            var areaModel = AreaMapper.MapToModel(thingDto.Area);

            var thingModel = new ThingModel(
                thingDto.Id,
                thingDto.Name,
                thingDto.Ip,
                thingDto.IsVideo,
                thingDto.IsAlerted,
                thingDto.Description,
                thingDto.AreaId,
                areaModel
            );

            return thingModel;
        }

        public static ThingDto MapToDto(ThingModel thingModel)
        {
            var areaDto = AreaMapper.MapToDto(thingModel.Area);

            var thingDto = new ThingDto
            {
                Id = thingModel.Id,
                Name = thingModel.Name,
                Ip = thingModel.Ip,
                IsVideo = thingModel.IsVideo,
                IsAlerted = thingModel.IsAlerted,
                Description = thingModel.Description,
                AreaId = thingModel.AreaId,
                Area = areaDto
            };

            return thingDto;
        }
    }
}
