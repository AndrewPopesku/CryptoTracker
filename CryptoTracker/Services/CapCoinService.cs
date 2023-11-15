using CryptoTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class CapCoinService
    {
        private HttpClient _httpClient;
        private static string _baseUrl = "https://api.coincap.io/v2/";

        public CapCoinService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<CryptoCurrency>> GetCryptoCurrencies(int quantity)
        {
            var response = await _httpClient.GetStringAsync(_baseUrl + "assets");
            var jsonData = JObject.Parse(response);
            var cryptoCurrencies = JsonConvert.DeserializeObject<List<CryptoCurrency>>(jsonData["data"].ToString());

            return cryptoCurrencies;
        }
    }
}