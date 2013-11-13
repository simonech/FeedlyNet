using System.Runtime.Remoting.Messaging;
using System.Security.Authentication;
using System.Threading;
using Feedly.NET.Model;
using Feedly.NET.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedly.NET
{
    public class FeedlyClient: BaseClient
    {
        public FeedlyClient(string _oAuthCode, UrlBuilder _urlBuilder): base(_urlBuilder)
        {
            oAuthCode = _oAuthCode;
        }

        public async Task<Profile> GetProfile()
        {
            return await ExecGet<Profile>(UrlBuilder.GetProfileUrl());
        }



        public async Task<Profile> UpdateProfile(Profile profile)
        {
            string json = JsonConvert.SerializeObject(profile);
            return await ExecPost<Profile>(UrlBuilder.GetProfileUrl(),json);
        }


        public async Task<Dictionary<string, string>> GetPreferences()
        {
            return await ExecGet<Dictionary<string, string>>(UrlBuilder.GetPreferencesUrl());
        }


        public async Task<Dictionary<string, string>> UpdatePreference(string name, string value)
        {
            Dictionary<string,string> pref = new Dictionary<string,string>();
            pref.Add(name, value);
            string json = JsonConvert.SerializeObject(pref);
            return await ExecPost<Dictionary<string, string>>(UrlBuilder.GetPreferencesUrl(), json);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await ExecGet<List<Category>>(UrlBuilder.GetCategoriesUrl());
        }

        public async Task<bool> UpdateCategory(string id, string label)
        {
            Category cat = new Category() { label = label};
            string json = JsonConvert.SerializeObject(cat);

            return await ExecPost(UrlBuilder.GetCategoryUrl(id), json);
        }

        public async Task<bool> DeleteCategory(string id)
        {
            return await ExecDelete(UrlBuilder.GetCategoryUrl(id));
        }


        public async Task<List<Subscription>> GetSubscriptions()
        {
            return await ExecGet<List<Subscription>>(UrlBuilder.GetSubscriptionsUrl());
        }

        public async Task<bool> AddOrUpdateSubscription(Subscription subscription)
        {
            string json = JsonConvert.SerializeObject(subscription);
            return await ExecPost(UrlBuilder.GetSubscriptionsUrl(), json);
        }

        public async Task<bool> DeleteSubscription(string feedId)
        {
            return await ExecDelete(UrlBuilder.GetSubscriptionUrl(feedId));
        }

        public async Task<List<Topic>> GetTopics()
        {
            return await ExecGet<List<Topic>>(UrlBuilder.GetTopicsUrl());
        }

        public async Task<bool> AddOrUpdateTopic(Topic newTopic)
        {
            string json = JsonConvert.SerializeObject(newTopic);
            return await ExecPost(UrlBuilder.GetTopicsUrl(), json);
        }

        public async Task<bool> DeleteTopic(string topicId)
        {
            return await ExecDelete(UrlBuilder.GetTopicUrl(topicId));
        }

        public async Task<List<Tag>> GetTags()
        {
            return await ExecGet<List<Tag>>(UrlBuilder.GetTagsUrl());
        }

        public async Task<bool> UpdateTag(string id, string label)
        {
            Tag tag = new Tag() { label = label };
            string json = JsonConvert.SerializeObject(tag);

            return await ExecPost(UrlBuilder.GetTagUrl(id), json);
        }

        public async Task<bool> DeleteTag(string tagId)
        {
            return await ExecDelete(UrlBuilder.GetTagUrl(tagId));
        }

        public async Task<Feed> GetFeed(string feedId)
        {
            return await ExecGet<Feed>(UrlBuilder.GetFeedUrl(feedId));
        }

        public async Task<List<Feed>> GetFeeds(string[] feedIds)
        {
            string json = JsonConvert.SerializeObject(feedIds);
            return await ExecPost<List<Feed>>(UrlBuilder.GetFeedsUrl(), json);
        }

        public async Task<UnreadCount> GetUnreadCount()
        {
            return await ExecGet<UnreadCount>(UrlBuilder.GetMarkersCountUrl());
        }
    }
}
