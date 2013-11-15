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
        
        private const string _markersPart = "markers/";

        private const string _streamsPart = "streams/";

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


        public Uri GetMarkersUrl()
        {
            return new Uri(_serviceUrl, _markersPart);
        }

        public Uri GetMarkersCountUrl(long newerThan = 0, bool autorefresh = false, string streamId = "")
        {
            string parameters = string.Empty;
            if (newerThan != 0) parameters += "&newerThan="+newerThan;
            if (autorefresh) parameters += "&autorefresh=true";
            if (!String.IsNullOrEmpty(streamId)) parameters += "&streamId=" + streamId;
            if (!String.IsNullOrWhiteSpace(parameters))
            {
                parameters = parameters.TrimStart('&');
                parameters = "?" + parameters;
            }
                

            return new Uri(GetMarkersUrl(), "counts"+parameters);
        }

        public Uri GetStreamIdsUrl(string streamId, int count = 0, string ranked = "", long newerThan = 0,
            bool unreadOnly = false, string continuation = "")
        {
            return GetStreamUrl("ids", streamId, count: count, ranked: ranked, newerThan: newerThan,
                unreadOnly: unreadOnly, continuation: continuation);
        }

        public Uri GetStreamContentsUrl(string streamId, int count = 0, string ranked = "", long newerThan = 0,
    bool unreadOnly = false, string continuation = "")
        {
            return GetStreamUrl("contents", streamId, count: count, ranked: ranked, newerThan: newerThan,
                unreadOnly: unreadOnly, continuation: continuation);
        }

        private Uri GetStreamUrl(string resultType,string streamId, int count = 0, string ranked = "", long newerThan = 0, bool unreadOnly = false, string continuation = "")
        {
            string streamIdEscaped = Uri.EscapeDataString(streamId);

            string parameters = string.Empty;
            if (count != 0) parameters += "&count=" + count;
            if (newerThan != 0) parameters += "&newerThan=" + newerThan;
            if (unreadOnly) parameters += "&unreadOnly=true";
            if (!String.IsNullOrEmpty(ranked)) parameters += "&ranked=" + ranked;
            if (!String.IsNullOrEmpty(continuation)) parameters += "&continuation=" + continuation;
            if (!String.IsNullOrWhiteSpace(parameters))
            {
                parameters = parameters.TrimStart('&');
                parameters = "?" + parameters;
            }

            return new Uri(_serviceUrl, _streamsPart + streamIdEscaped + "/" + resultType + parameters);
        }
    }
}
