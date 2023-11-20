using CryptoTracker.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class ApiService
    {
        protected HttpClient _httpClient;

        public ApiService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<string> GetJsonDataFromEndpoint(string endpoint, string parentNode = "data")
        {
            try
            {
                var response = await _httpClient.GetStringAsync(endpoint);
                var rawJson = JObject.Parse(response);
                return rawJson[parentNode].ToString();
            }
            catch (HttpRequestException ex)
            {
                throw new FetchDataException("Error: No internet connection or server not available", ex);
            }
            catch (Exception ex)
            {
                throw new FetchDataException($"Error: Error fetching data from endpoint '{_httpClient.BaseAddress + endpoint}'", ex);
            }
        }
    }
}
