using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Services
{
    public class ResourceIdsBuilder
    {
        private string UserId { get; set; }

        public string CATEGORY_MUST_READ { get { return UserResourceId("category", "global.must"); } }
        public string CATEGORY_ALL { get { return UserResourceId("category", "global.all"); } }
        public string CATEGORY_UNCATEGORIZED { get { return UserResourceId("category", "global.uncategorized"); } }
        public string TAG_RECENTLY_READ { get { return UserResourceId("tag", "global.read"); } }
        public string TAG_SAVED_FOR_LATER { get { return UserResourceId("tag", "global.saved"); } }

        public ResourceIdsBuilder(string userId)
        {
            UserId = userId;
        }

        public string CategoryId(string categoryLabel)
        {
            return UserResourceId("category", categoryLabel);
        }

        public string TagId(string tagName)
        {
            return UserResourceId("tag", tagName);
        }

        public string TopicId(string topicName)
        {
            return UserResourceId("topic", topicName);
        }

        public string FeedId(string feedUrl)
        {
            return string.Format("feed/{0}", feedUrl);
        }


        private string UserResourceId(string type, string id)
        {
            return string.Format("user/{0}/{1}/{2}", UserId, type, id);
        }
    }
}
