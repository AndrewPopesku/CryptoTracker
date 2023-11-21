using CryptoTracker.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    /// <summary>
    /// Provides methods to retrieve cryptocurrency data from the CapCoin API.
    /// </summary>
    public class CapCoinService : ApiService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapCoinService"/> class with a base URL.
        /// </summary>
        public CapCoinService(string baseUrl) : base(baseUrl) { }

        /// <summary>
        /// Retrieves a list of top cryptocurrencies based on quantity.
        /// </summary>
        /// <param name="quantity">The number of top cryptocurrencies to retrieve.</param>
        /// <returns>A list of top cryptocurrencies.</returns>
        public async Task<List<CryptoCurrency>> GetTopCryptoCurrencies(int quantity)
        {
            var endpoint = "assets";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrencies = JsonConvert.DeserializeObject<List<CryptoCurrency>>(jsonString);

            return cryptoCurrencies.OrderBy(crypto => crypto.Rank).Take(quantity).ToList();
        }

        /// <summary>
        /// Retrieves cryptocurrency details by ID.
        /// </summary>
        /// <param name="id">The ID of the cryptocurrency.</param>
        /// <returns>The cryptocurrency details.</returns>
        public async Task<CryptoCurrency> GetCryptoCurrencyById(string id)
        {
            var endpoint = $"assets/{id}";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrency = JsonConvert.DeserializeObject<CryptoCurrency>(jsonString);

            return cryptoCurrency;
        }

        /// <summary>
        /// Retrieves cryptocurrencies by name or symbol.
        /// </summary>
        /// <param name="filterString">The string to filter cryptocurrencies by name or symbol.</param>
        /// <returns>A list of cryptocurrencies matching the filter criteria.</returns>
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

        /// <summary>
        /// Retrieves historical price data for a cryptocurrency by ID.
        /// </summary>
        /// <param name="id">The ID of the cryptocurrency.</param>
        /// <returns>A list of price history entries for the cryptocurrency.</returns>
        public async Task<List<PriceHistoryEntry>> GetCryptoCurrencyHistoryById(string id)
        {
            var endpoint = $"assets/{id}/history?interval=d1";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoPriceHistoryEntries = JsonConvert.DeserializeObject<List<PriceHistoryEntry>>(jsonString);

            return cryptoPriceHistoryEntries.TakeLast(20).ToList();
        }
    }
}