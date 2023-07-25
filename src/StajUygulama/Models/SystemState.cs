using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StajUygulama.Models
{
    [Serializable]
    internal class SystemState
    {
        public List<Device<float>> analogDeviceList { get; set; }
        public List<Device<bool>> digitalDeviceList { get; set; }
        [JsonInclude]
        public uint lastTopic;

        public SystemState()
        {
            analogDeviceList = new List<Device<float>>();
            digitalDeviceList = new List<Device<bool>>();
            lastTopic = 0;
        }

        public uint getLastTopicId()
        {
            return ++lastTopic;
        }

    }
}
