using System;
using System.Reflection.Emit;
using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;

namespace Feedly.NET.Model
{
    public class Topic
    {
        public string id { get; set; }
        public Interest interest { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime updated { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime created { get; set; }

        public enum Interest
        {
            LOW,
            MEDIUM,
            HIGH
        }

        public string Label 
        {
            get
            {
                return id.Split('/')[3];
            }
        }
    }

    
}