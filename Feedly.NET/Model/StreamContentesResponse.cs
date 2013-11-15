using System;
using System.Collections.Generic;
using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;

namespace Feedly.NET.Model
{
    public class StreamContentesResponse
    {
        public string id { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime updated { get; set; }
        public string continuation { get; set; }
        public List<Entry> items { get; set; }
    }
}