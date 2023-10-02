using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shyryi_WatchForYou.Mappers
{
    public static class AreaMapper
    {
        public static AreaModel MapToModel(AreaDto areaDto)
        {
            var userModel = UserMapper.MapToModel(areaDto.User);

            var areaModel = new AreaModel(
                areaDto.Id,
                areaDto.Name,
                areaDto.Description,
                areaDto.UserId,
                userModel
            );

            if (areaDto.Things != null)
            {
                areaModel.Things = areaDto.Things.Select(thing => ThingMapper.MapToModel(thing)).ToList();
            }

            return areaModel;
        }

        public static AreaDto MapToDto(AreaModel areaModel)
        {
            var userDto = UserMapper.MapToDto(areaModel.User);

            var areaDto = new AreaDto
            {
                Id = areaModel.Id,
                Name = areaModel.Name,
                Description = areaModel.Description,
                UserId = areaModel.UserId,
                User = userDto
            };

            if (areaModel.Things != null)
            {
                areaDto.Things = areaModel.Things.Select(thing => ThingMapper.MapToDto(thing)).ToList();
            }

            return areaDto;
        }

        public static List<AreaModel> MapToModel(List<AreaDto> areaDtos)
        {
            return areaDtos.Select(MapToModel).ToList();
        }

        public static List<AreaDto> MapToDto(List<AreaModel> areaModels)
        {
            return areaModels.Select(MapToDto).ToList();
        }
    }
}
