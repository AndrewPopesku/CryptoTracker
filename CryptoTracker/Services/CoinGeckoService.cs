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
    public class CoinGeckoService : ApiService
    {
        public CoinGeckoService(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<List<Ticker>> GetTickersByCurrencyId(string currencyId)
        {
            var endpoint = $"coins/{currencyId}/tickers";
            var jsonString = await GetJsonDataFromEndpoint(endpoint, "tickers");

            var tickers = JsonConvert.DeserializeObject<List<Ticker>>(jsonString);
            return tickers;
        }
    }
}
