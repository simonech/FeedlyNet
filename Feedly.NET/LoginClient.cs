using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feedly.NET.Model;
using Feedly.NET.Services;

namespace Feedly.NET
{
    public class LoginClient : BaseClient
    {
        private const string _clientId = "sandbox";
        private const string _clientSecret = "Z5ZSFRASVWCV3EFATRUY";
        private const string _redirectUrl = "http://localhost";
        private const string _authScope = "https://cloud.feedly.com/subscriptions";

        public LoginClient(UrlBuilder urlBuilder) : base(urlBuilder) { }

        public Uri GetLoginUrl(string state=null)
        {
            Uri baseUri = new Uri(UrlBuilder.GetAuthorizationUrl(),"auth");

            string queryString = "?response_type=code";
            queryString += "&client_id=" + _clientId;
            queryString += "&redirect_uri=" + _redirectUrl;
            queryString += "&scope=" + Uri.EscapeDataString(_authScope);
            if(state!=null)
                queryString += "&state=" + state;

            return new Uri(baseUri,queryString);
        }

        public async Task<AuthenticationInfo> GetAuthorizationToken(string code, string state = null)
        {
            Uri baseUri = new Uri(UrlBuilder.GetAuthorizationUrl(), "token");

            string queryString = "?code="+code;
            queryString += "&client_id=" + _clientId;
            queryString += "&client_secret=" + _clientSecret;
            queryString += "&redirect_uri=" + _redirectUrl;
            queryString += "&grant_type=authorization_code";
            if (state != null)
                queryString += "&state=" + state;

            return await ExecPost<AuthenticationInfo>(new Uri(baseUri, queryString), "");
        }


        public async Task<AuthenticationInfo> RefreshAuthorizationToken(string refreshToken)
        {
            Uri baseUri = new Uri(UrlBuilder.GetAuthorizationUrl(), "token");

            string queryString = "?refresh_token=" + refreshToken;
            queryString += "&client_id=" + _clientId;
            queryString += "&client_secret=" + _clientSecret;
            queryString += "&grant_type=refresh_token";

            return await ExecPost<AuthenticationInfo>(new Uri(baseUri, queryString), "");
        }

    }
}
