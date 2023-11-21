using CryptoTracker.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    /// <summary>
    /// Represents a service to interact with APIs for retrieving data.
    /// </summary>
    public class ApiService
    {
        protected HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiService"/> class with a base URL.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API.</param>
        public ApiService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Retrieves JSON data from a specified endpoint and extracts data from a specified parent node.
        /// </summary>
        /// <param name="endpoint">The API endpoint to retrieve data from.</param>
        /// <param name="parentNode">The parent node within the JSON response (default is "data").</param>
        /// <returns>A string representing JSON data from the specified endpoint.</returns>
        /// <exception cref="FetchDataException">Thrown when there is an error fetching data from the endpoint.</exception>
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
