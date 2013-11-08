using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Services
{
    public class UrlBuilder
    {
        private readonly ResourceIdsBuilder _resourceIdsBuilder;
        private Uri _serviceUrl = new Uri("http://sandbox.feedly.com/v3/");
        private Uri _serviceUrlSSL = new Uri("https://sandbox.feedly.com/v3/");


        private const string _authPart = "auth/";

        private const string _profilePart = "profile/";

        private const string _preferencesPart = "preferences/";

        private const string _categoriesPart = "categories/";

        private const string _subscriptionsPart = "subscriptions/";

        private const string _topicsPart = "topics/";

        private const string _tagsPart = "tags/";

        private const string _feedsPart = "feeds/";

        public UrlBuilder(ResourceIdsBuilder resourceIdsBuilder)
        {
            _resourceIdsBuilder = resourceIdsBuilder;
        }

        public Uri GetAuthorizationUrl()
        {
            return new Uri(_serviceUrlSSL, _authPart);
        }

        public Uri GetProfileUrl()
        {
            return new Uri(_serviceUrlSSL, _profilePart);
        }

        public Uri GetPreferencesUrl()
        {
            return new Uri(_serviceUrl, _preferencesPart);
        }

        public Uri GetCategoriesUrl()
        {
            return new Uri(_serviceUrl, _categoriesPart);
        }

        public Uri GetCategoryUrl(string categoryId)
        {
            string categoryIdEscaped = Uri.EscapeDataString(categoryId);
            return new Uri(GetCategoriesUrl(), categoryIdEscaped);
        }

        public Uri GetSubscriptionsUrl()
        {
            return new Uri(_serviceUrlSSL, _subscriptionsPart);
        }

        public Uri GetSubscriptionUrl(string feedId)
        {
            string feedIdEscaped = Uri.EscapeDataString(feedId);
            return new Uri(GetSubscriptionsUrl(), feedIdEscaped);
        }

        public Uri GetTopicsUrl()
        {
            return new Uri(_serviceUrl, _topicsPart);
        }

        public Uri GetTopicUrl(string topicId)
        {
            string topicIdEscaped = Uri.EscapeDataString(topicId);
            return new Uri(GetTopicsUrl(), topicIdEscaped);
        }

        public Uri GetTagsUrl()
        {
            return new Uri(_serviceUrl, _tagsPart);
        }

        public Uri GetTagUrl(string tagId)
        {
            string tagIdEscaped = Uri.EscapeDataString(tagId);
            return new Uri(GetTagsUrl(), tagIdEscaped);
        }

        private Uri GetBaseFeedsUrl()
        {
            return new Uri(_serviceUrl, _feedsPart);
        }

        public Uri GetFeedsUrl()
        {
            return new Uri(GetBaseFeedsUrl(), ".mget");
        }

        public Uri GetFeedUrl(string feedId)
        {
            string feedIdEscaped = Uri.EscapeDataString(feedId);
            return new Uri(GetBaseFeedsUrl(), feedIdEscaped);
        }
    }
}
