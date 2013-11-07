using System;
using System.Collections.Generic;
using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;

namespace Feedly.NET.Model
{
    public class Subscription
    {
        public Subscription()
        {
            topics = new List<string>();
            categories=new List<Category>();
        }

        public string id { get; set; }
        public string title { get; set; }
        public string website { get; set; }
        public List<Category> categories { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime updated { get; set; }
        public double velocity { get; set; }
        public List<string> topics { get; set; }
        public string state { get; set; }
    }
}