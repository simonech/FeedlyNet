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
        private readonly UrlBuilder _urlBuilder;

        public FeedlyClient(string _oAuthCode, UrlBuilder urlBuilder)
        {
            oAuthCode = _oAuthCode;
            _urlBuilder = urlBuilder;
        }

        public async Task<Profile> GetProfile()
        {
            return await ExecGet<Profile>(_urlBuilder.GetProfileUrl());
        }



        public async Task<Profile> UpdateProfile(Profile profile)
        {
            string json = JsonConvert.SerializeObject(profile);
            return await ExecPost<Profile>(_urlBuilder.GetProfileUrl(),json);
        }


        public async Task<Dictionary<string, string>> GetPreferences()
        {
            return await ExecGet<Dictionary<string, string>>(_urlBuilder.GetPreferencesUrl());
        }


        public async Task<Dictionary<string, string>> UpdatePreference(string name, string value)
        {
            Dictionary<string,string> pref = new Dictionary<string,string>();
            pref.Add(name, value);
            string json = JsonConvert.SerializeObject(pref);
            return await ExecPost<Dictionary<string, string>>(_urlBuilder.GetPreferencesUrl(), json);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await ExecGet<List<Category>>(_urlBuilder.GetCategoriesUrl());
        }

        public async Task<bool> UpdateCategory(string id, string label)
        {
            Category cat = new Category() { label = label};
            string json = JsonConvert.SerializeObject(cat);

            return await ExecPost(_urlBuilder.GetCategoryUrl(id), json);
        }

        public async Task<bool> DeleteCategory(string id)
        {
            return await ExecDelete(_urlBuilder.GetCategoryUrl(id));
        }


        public async Task<List<Subscription>> GetSubscriptions()
        {
            return await ExecGet<List<Subscription>>(_urlBuilder.GetSubscriptionsUrl());
        }

        public async Task<bool> AddOrUpdateSubscription(Subscription subscription)
        {
            string json = JsonConvert.SerializeObject(subscription);
            return await ExecPost(_urlBuilder.GetSubscriptionsUrl(), json);
        }

        public async Task<bool> DeleteSubscription(string feedId)
        {
            return await ExecDelete(_urlBuilder.GetSubscriptionUrl(feedId));
        }

        public async Task<List<Topic>> GetTopics()
        {
            return await ExecGet<List<Topic>>(_urlBuilder.GetTopicsUrl());
        }

        public async Task<bool> AddOrUpdateTopic(Topic newTopic)
        {
            string json = JsonConvert.SerializeObject(newTopic);
            return await ExecPost(_urlBuilder.GetTopicsUrl(), json);
        }

        public async Task<bool> DeleteTopic(string topicId)
        {
            return await ExecDelete(_urlBuilder.GetTopicUrl(topicId));
        }

        public async Task<List<Tag>> GetTags()
        {
            return await ExecGet<List<Tag>>(_urlBuilder.GetTagsUrl());
        }

        public async Task<bool> UpdateTag(string id, string label)
        {
            Tag tag = new Tag() { label = label };
            string json = JsonConvert.SerializeObject(tag);

            return await ExecPost(_urlBuilder.GetTagUrl(id), json);
        }

        public async Task<bool> DeleteTag(string tagId)
        {
            return await ExecDelete(_urlBuilder.GetTagUrl(tagId));
        }

        public async Task<Feed> GetFeed(string feedId)
        {
            return await ExecGet<Feed>(_urlBuilder.GetFeedUrl(feedId));
        }

        public async Task<List<Feed>> GetFeeds(string[] feedIds)
        {
            string json = JsonConvert.SerializeObject(feedIds);
            return await ExecPost<List<Feed>>(_urlBuilder.GetFeedsUrl(), json);
        }
    }
}
