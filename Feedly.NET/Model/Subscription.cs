using System;
using System.Collections.Generic;
using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;

namespace Feedly.NET.Model
{
    public class Subscription: Feed
    {
        public Subscription()
        {
            categories=new List<Category>();
        }
        public List<Category> categories { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime updated { get; set; }
    }
}