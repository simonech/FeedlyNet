using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Feedly.NET.Exceptions;
using Feedly.NET.Model;
using Feedly.NET.Services;
using Newtonsoft.Json;

namespace Feedly.NET
{
    public abstract class BaseClient
    {
        public UrlBuilder UrlBuilder { get; set; }
        public string oAuthCode { get; set; }

        protected BaseClient(UrlBuilder urlBuilder)
        {
            UrlBuilder = urlBuilder;
        }

        protected HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", oAuthCode);
            return client;
        }
        protected async Task<T> ExecGet<T>(Uri requestUrl)
        {
            var client = GetHttpClient();

            var response = await client.GetAsync(requestUrl);

            return await ProcessResult<T>(response);
        }

        protected async Task<T> ExecPost<T>(Uri requestUrl, string jsonContent)
        {
            var client = GetHttpClient();
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            return await ProcessResult<T>(response);
        }

        protected async Task<bool> ExecPost(Uri requestUrl, string jsonContent)
        {
            var client = GetHttpClient();
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            return await ProcessResult(response);
        }

        protected async Task<bool> ExecDelete(Uri requestUrl)
        {
            var client = GetHttpClient();
            HttpResponseMessage response = await client.DeleteAsync(requestUrl);
            return await ProcessResult(response);
        }

        private static async Task<bool> ProcessResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw ProcessServerError(response).Result;
        }

        private static async Task<T> ProcessResult<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            throw ProcessServerError(response).Result;
        }

        private static async Task<Exception> ProcessServerError(HttpResponseMessage response)
        {
            String json = await response.Content.ReadAsStringAsync();
            ErrorMessage message = JsonConvert.DeserializeObject<ErrorMessage>(json);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new FeedlyAuthenticationException(String.Format("Authentication error: {0}", message.errorMessage));
            if (response.StatusCode == HttpStatusCode.BadRequest)
                return new FeedlyClientErrorException(String.Format("Request execution error: {0}", message.errorMessage));
            if (message != null)
            {
                return new FeedlyUnspecifiedException(String.Format("Request execution error: {0}", message.errorMessage));
            }
            return new FeedlyUnspecifiedException(String.Format("Generic error: {0}{1}", response.StatusCode,
                response.ReasonPhrase));
        }
    }
}