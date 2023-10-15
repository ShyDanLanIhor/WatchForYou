using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Repositories;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou_Server.Models;

namespace Shyryi_WatchForYou.Models.Repositories
{
    public static class UserRepository
    {
        private static readonly object userRepositoryLock = new object();
        public static bool AddUser(UserDto user)
        {
            lock (userRepositoryLock)
            {
                TcpClientService.Write(JsonConvert.SerializeObject(
                    new RequestEntity
                    {
                        Type = "Create account",
                        Data = JsonConvert.SerializeObject(user)
                    }));
                return TcpClientService.Read<bool>() == true;
            }
        }

        public static UserDto GetByUsername(string username)
        {
            lock (userRepositoryLock)
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
        }

        public static UserDto GetByEmail(string email)
        {
            lock (userRepositoryLock)
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
        }

        public static UserDto GetUsersByUsernameAndPassword(NetworkCredential credential)
        {
            lock (userRepositoryLock)
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
                return user;
            }
        }

        public static bool RemoveUserByUsername(string username)
        {
            lock (userRepositoryLock)
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
        }

        public static bool UpdateUser(int userId, UserDto updatedUser)
        {
            lock (userRepositoryLock)
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
        }

        public static bool UpdateUserPassword(UserDto updatedUser, string password)
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

        public static List<ThingDto> GetAllThingsByUser(int userId)
        {
            lock (userRepositoryLock)
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
        }
    }
}

