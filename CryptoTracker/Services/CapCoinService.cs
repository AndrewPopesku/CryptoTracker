using CryptoTracker.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class CapCoinService : ApiService
    {
        public CapCoinService(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<List<CryptoCurrency>> GetTopCryptoCurrencies(int quantity)
        {
            var endpoint = "assets";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrencies = JsonConvert.DeserializeObject<List<CryptoCurrency>>(jsonString);

            return cryptoCurrencies.OrderBy(crypto => crypto.Rank).Take(quantity).ToList();
        }

        public async Task<CryptoCurrency> GetCryptoCurrencyById(string id)
        {
            var endpoint = $"assets/{id}";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrency = JsonConvert.DeserializeObject<CryptoCurrency>(jsonString);

            return cryptoCurrency;
        }

        public async Task<List<CryptoCurrency>> GetCryptoCurrenciesByNameOrSymbol(string filterString)
        {
            var endpoint = "assets";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrencies = JsonConvert.DeserializeObject<List<CryptoCurrency>>(jsonString);

            var normalizedFilterString = filterString.ToLower();
            return cryptoCurrencies
                .Where(crypto => crypto.Name.ToLower().Contains(normalizedFilterString) 
                    || crypto.Symbol.ToLower().Contains(normalizedFilterString))
                .ToList();
        }

        public async Task<List<PriceHistoryEntry>> GetCryptoCurrencyHistoryById(string id)
        {
            var endpoint = $"assets/{id}/history?interval=d1";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoPriceHistoryEntries = JsonConvert.DeserializeObject<List<PriceHistoryEntry>>(jsonString);

            return cryptoPriceHistoryEntries.TakeLast(20).ToList();
        }
    }
}