using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Services
{
    public static class TcpClientService
    {
        private static TcpClient client = new TcpClient("127.0.0.1", 12345);
        private static NetworkStream stream = client.GetStream();

        public static void Write(string request)
        {
            byte[] sendData = Encoding.UTF8.GetBytes(request);
            stream.Write(sendData, 0, sendData.Length);
        }

        public static void Write(UserDto user)
        {
            byte[] sendData = JsonMapper.ObjectToBytes(user);
            stream.Write(sendData, 0, sendData.Length);
        }

        public static void Write(AreaDto area)
        {
            byte[] sendData = JsonMapper.ObjectToBytes(area);
            stream.Write(sendData, 0, sendData.Length);
        }

        public static void Write(ThingDto thing)
        {
            byte[] sendData = JsonMapper.ObjectToBytes(thing);
            stream.Write(sendData, 0, sendData.Length);
        }
        public static void Write(RequestEntity request)
        {
            byte[] sendData = JsonMapper.ObjectToBytes(request);
            stream.Write(sendData, 0, sendData.Length);
        }

        public static T Read<T>()
        {
            byte[] receiveData = new byte[1024];
            int bytesRead = stream.Read(receiveData, 0, receiveData.Length);
            return JsonMapper.BytesToObject<T>(receiveData);
        }
    }
}
