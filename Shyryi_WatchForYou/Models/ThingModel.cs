using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Models
{
    public class ThingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public bool IsVideo { get; set; }
        public bool IsAlerted { get; set; }
        public string Description { get; set; }
        public int AreaId { get; set; }
        public AreaModel Area { get; set; }

        public ThingModel(int id, string name, string ip, bool isVideo, bool isAlerted, string description, int areaId, AreaModel area)
        {
            Id = id;
            Name = name;
            Ip = ip;
            IsVideo = isVideo;
            IsAlerted = isAlerted;
            Description = description;
            AreaId = areaId;
            Area = area;
        }

        public ThingModel(string name, string ip, bool isVideo, bool isAlerted, string description, int areaId, AreaModel area)
        {
            Name = name;
            Ip = ip;
            IsVideo = isVideo;
            IsAlerted = isAlerted;
            Description = description;
            AreaId = areaId;
            Area = area;
        }

        public ThingModel(string name, string ip, bool isVideo, bool isAlerted, string description, int areaId)
        {
            Name = name;
            Ip = ip;
            IsVideo = isVideo;
            IsAlerted = isAlerted;
            Description = description;
            AreaId = areaId;
        }

        public ThingModel(ThingModel other)
        {
            Id = other.Id;
            Name = other.Name;
            Ip = other.Ip;
            IsVideo = other.IsVideo;
            IsAlerted = other.IsAlerted;
            Description = other.Description;
            AreaId = other.AreaId;
            Area = new AreaModel(other.Area);
        }
    }
}
