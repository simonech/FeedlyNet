using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Model
{
    public class Profile
    {
        public string id { get; set; }
        public string email { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string locale { get; set; }
        public string reader { get; set; }
        public string google { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string wave { get; set; }
        public string client { get; set; }
        public bool wordPressConnected { get; set; }
        public bool evernoteConnected { get; set; }
        public bool pocketConnected { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime created { get; set; }

    }
}
