using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        //client(browser) that makes a http reques and is expecting a response
        private HttpClient _client;
        //logs the reuests and responses
        private ILogger<CustomHttpClient> _logger;

        public CustomHttpClient(ILogger<CustomHttpClient> logger)
        {
            //client(browser makes a http request) 
            //eg getevents , post events
            _client = new HttpClient();
            _logger = logger;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await _client.SendAsync(requestMessage);
        }

        public async Task<string> GetStringAsync(string uri)
        {

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();

        }


        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {

            return await DoPostPutAsync(HttpMethod.Post, uri, item);
        }


        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item);
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }


            // a new StringContent must be created for each retry 
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500 
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;

        }
    }
}
