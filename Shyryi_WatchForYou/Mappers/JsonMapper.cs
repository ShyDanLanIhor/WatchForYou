using Newtonsoft.Json;
using System.Text;

namespace Shyryi_WatchForYou.Mappers
{
    public static class JsonMapper
    {
        public static byte[] ObjectToBytes(object obj)
        {
            return Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(obj));
        }

        public static T BytesToObject<T>(byte[] bytes)
        {
            return JsonConvert.DeserializeObject<T>(
                Encoding.UTF8.GetString(bytes));
        }
    }
}
