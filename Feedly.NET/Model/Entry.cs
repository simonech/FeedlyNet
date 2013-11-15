using System;
using System.Collections.Generic;
using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;

namespace Feedly.NET.Model
{
    public class Entry
    {
        public string id { get; set; }
        public List<string> keywords { get; set; }
        public string fingerprint { get; set; }
        public string originId { get; set; }
        public string author { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime crawled { get; set; }
        public string title { get; set; }
        public Content summary { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime published { get; set; }
        public List<Alternate> alternate { get; set; }
        public List<Canonical> canonical { get; set; }
        public Origin origin { get; set; }
        public Visual visual { get; set; }
        public bool unread { get; set; }
        public List<Category> categories { get; set; }
        public string sid { get; set; }
        public int? engagement { get; set; }
        public double? engagementRate { get; set; }
        public Content content { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? updated { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? recrawled { get; set; }
    }

    public class Content
    {
        public string content { get; set; }
        public string direction { get; set; }
    }

    public class Visual
    {
        public int width { get; set; }
        public string url { get; set; }
        public int height { get; set; }
        public string contentType { get; set; }
    }

    public class Alternate
    {
        public string href { get; set; }
        public string type { get; set; }
    }

    public class Canonical
    {
        public string href { get; set; }
        public string type { get; set; }
    }

    public class Origin
    {
        public string htmlUrl { get; set; }
        public string streamId { get; set; }
        public string title { get; set; }
    }
}