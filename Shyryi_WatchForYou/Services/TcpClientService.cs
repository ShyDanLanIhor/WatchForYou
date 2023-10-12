using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou_Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Shyryi_WatchForYou.Services
{
    public static class TcpClientService
    {
        private static TcpClient client = new TcpClient("127.0.0.1", 12345);
        private static NetworkStream stream = client.GetStream();
        private static int additionalLength = 3;

        public static void Close()
        {
            stream.Close();
            client.Close();
        }
        public static void Write(string request)
        {
            byte[] sendData = Encoding.UTF8.GetBytes(request);
            Array.Resize(ref sendData, sendData.Length + additionalLength);
            stream.Write(sendData, 0, sendData.Length);
        }

        public static void Write<T>(T data)
        {
            byte[] sendData = JsonMapper.ObjectToBytes(data);
            Array.Resize(ref sendData, sendData.Length + additionalLength);
            stream.Write(sendData, 0, sendData.Length);
        }

        public static T Read<T>()
        {
            List<byte> receiveDataList = new List<byte>();
            int stopBytes = 0;
            int bytesRead = 0;
            byte[] receiveData = new byte[1];
            while (stopBytes < additionalLength)
            {
                stream.Read(receiveData, 0, 1);
                bytesRead = receiveData[0];
                receiveDataList.Add((byte)bytesRead);
                if (bytesRead == 0)
                { stopBytes++; }
                else { stopBytes = 0; }
            }
            return JsonMapper.BytesToObject<T>(receiveDataList.ToArray());
        }
    }
}
