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
    /// <summary>
    /// Provides methods to interact with the CoinGecko API for retrieving cryptocurrency data.
    /// </summary>
    public class CoinGeckoService : ApiService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoService"/> class with a base URL.
        /// </summary>
        /// <param name="baseUrl">The base URL of the CoinGecko API.</param>
        public CoinGeckoService(string baseUrl) : base(baseUrl)
        {
        }

        /// <summary>
        /// Retrieves a list of tickers by currency ID.
        /// </summary>
        /// <param name="currencyId">The ID of the currency for which tickers will be retrieved.</param>
        /// <returns>A list of tickers associated with the specified currency ID.</returns>
        /// <exception cref="FetchDataException">Thrown when there is an error fetching data from the endpoint.</exception>
        public async Task<List<Ticker>> GetTickersByCurrencyId(string currencyId)
        {
            var endpoint = $"coins/{currencyId}/tickers";
            var jsonString = await GetJsonDataFromEndpoint(endpoint, "tickers");

            var tickers = JsonConvert.DeserializeObject<List<Ticker>>(jsonString);
            return tickers;
        }
    }
}
