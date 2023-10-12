using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou_Server.Models;

namespace Shyryi_WatchForYou.Services.ModelServices
{
    public static class UserService
    {
        public static bool CreateAccount(UserDto user)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Create account",
                        Data = JsonConvert.SerializeObject(user)
                    }));
                return TcpClientService.Read<bool>() == true;
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }
        public static UserDto GetByUsername(string username)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Get by username",
                        Data = new
                        {
                            Username = username
                        }
                    }));
                return TcpClientService.Read<UserDto>();
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<UserDto>(ex);
            }
        }
        public static UserDto GetByEmail(string email)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Get by email",
                        Data = new
                        {
                            Email = email
                        }
                    }));
                return TcpClientService.Read<UserDto>();
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<UserDto>(ex);
            }
        }

        public static bool AuthenticateUser(NetworkCredential credential)
        {
            TcpClientService.Write(JsonConvert.SerializeObject(
                new RequestEntity
                {
                    Type = "Authenticate user",
                    Data = new 
                    { 
                        Username = credential.UserName, 
                        Password = credential.Password
                    }
                }));
            UserDto user = TcpClientService.Read<UserDto>();
            if (user == null) { throw new InvalidDataInputException("Invalid username or password"); }
            if (user.IsVerificated == false) { throw new InvalidDataInputException("Email is not verificated"); }
            return true;
        }

        public static bool DeleteAccount(string username)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Delete account",
                        Data = new
                        {
                            Username = username
                        }
                    }));
                return TcpClientService.Read<bool>() == true;
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }

        public static bool UpdateUser(int userId, UserDto updatedUser)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Update user",
                        Data = JsonConvert.SerializeObject(
                            new
                            {
                                UserId = userId,
                                UpdatedUser = updatedUser
                            })
                    }));
                return TcpClientService.Read<bool>() == true;
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }

        public static bool UpdateUserPassword(UserDto updatedUser, string password)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Update user password",
                        Data = JsonConvert.SerializeObject(
                            new
                            {
                                Password = password,
                                UpdatedUser = updatedUser
                            })
                    }));
                return TcpClientService.Read<bool>() == true;
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<bool>(ex);
            }
        }

        public static List<ThingDto> GetAllThingsByUser(int userId)
        {
            try
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Get all things by user",
                        Data = new
                        {
                           UserId = userId
                        }
                    }));
                return TcpClientService.Read<List<ThingDto>>();
            }
            catch (Exception ex)
            {
                return ExceptionService
                    .HandleDataBaseException<List<ThingDto>>(ex);
            }
        }
    }
}