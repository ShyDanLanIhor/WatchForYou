using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Mappers
{
    public static class UserMapper
    {
        public static UserModel MapToModel(UserDto userDto)
        {
            var userModel = new UserModel(
                userDto.Id,
                userDto.Username,
                userDto.Password,
                userDto.FirstName,
                userDto.LastName,
                userDto.Email,
                userDto.IsVerificated
            );

            if (userDto.Areas != null)
            {
                userModel.Areas = userDto.Areas.Select(area => AreaMapper.MapToModel(area)).ToList();
            }

            return userModel;
        }

        public static UserDto MapToDto(UserModel userModel)
        {
            var userDto = new UserDto
            {
                Id = userModel.Id,
                Username = userModel.Username,
                Password = userModel.Password,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                IsVerificated = userModel.IsVerificated
            };

            if (userModel.Areas != null)
            {
                userDto.Areas = userModel.Areas.Select(area => AreaMapper.MapToDto(area)).ToList();
            }

            return userDto;
        }
    }
}
