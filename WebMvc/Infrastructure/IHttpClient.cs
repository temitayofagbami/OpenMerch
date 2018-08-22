using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    //IHttpClient is interface that defines the contract of the methods of HTTP CLient 
    //HTTP Client performs HTTP requests to and from browswer
    
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);

        Task<HttpResponseMessage> DeleteAsync(string uri);

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item);
    }
}
