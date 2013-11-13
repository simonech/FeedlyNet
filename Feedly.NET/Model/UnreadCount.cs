using System;
using System.Collections.Generic;
using Feedly.NET.JsonSerialization;
using Newtonsoft.Json;

namespace Feedly.NET.Model
{
    public class UnreadCountItem
    {
        public string id { get; set; }
        public int count { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime updated { get; set; }

        public FeedType Type
        {
            get
            {
                if(id.StartsWith("feed/")) return FeedType.Feed;
                if(id.EndsWith("category/global.all")) return FeedType.All;
                if (id.EndsWith("category/global.must")) return FeedType.MustRead;
                if (id.EndsWith("category/global.uncategorized")) return FeedType.Uncategorized;
                if(id.Contains("/category/")) return FeedType.Category;
                return FeedType.Undefined;
            }
        }
    }

    public class UnreadCount
    {
        public List<UnreadCountItem> unreadcounts { get; set; }
    }

    public enum FeedType
    {
        Undefined,
        All,
        MustRead,
        Uncategorized,
        Category,
        Feed
    }
}