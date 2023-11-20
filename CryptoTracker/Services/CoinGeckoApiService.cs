using CryptoTracker.Exceptions;
using CryptoTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class CoinGeckoApiService
    {
        private readonly HttpClient _httpClient;

        public CoinGeckoApiService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<Ticker>> GetTickersByCurrencyId(string currencyId)
        {
            var endpoint = "coins/" + currencyId + "/tickers";
            try
            {
                var response = await _httpClient.GetStringAsync(endpoint);
                var jsonString = JObject.Parse(response)["tickers"].ToString();

                var tickers = JsonConvert.DeserializeObject<List<Ticker>>(jsonString);
                return tickers;
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

        private async Task<string> GetJsonDataFromEndpoint(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(endpoint);
                var rawJson = JObject.Parse(response);
                return rawJson["data"].ToString();
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
