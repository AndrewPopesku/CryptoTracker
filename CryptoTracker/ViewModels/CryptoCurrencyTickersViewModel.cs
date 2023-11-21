using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using System;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    /// <summary>
    /// Represents a ViewModel responsible for displaying tickers of a specific cryptocurrency.
    /// </summary>
    public class CryptoCurrencyTickersViewModel
    {
        private readonly CoinGeckoService _coinGeckoApiService;

        /// <summary>
        /// Gets or sets the collection of tickers for the cryptocurrency.
        /// </summary>
        public ObservableCollection<Ticker> Tickers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrencyTickersViewModel"/> class.
        /// </summary>
        /// <param name="coinGeckoApiService">An instance of CoinGeckoService to retrieve tickers.</param>
        /// <param name="currencyId">The ID of the cryptocurrency for which tickers are displayed.</param>
        public CryptoCurrencyTickersViewModel(CoinGeckoService coinGeckoApiService, string currencyId)
        {
            _coinGeckoApiService = coinGeckoApiService ?? throw new ArgumentNullException(nameof(coinGeckoApiService));

            Tickers = new ObservableCollection<Ticker>();
            LoadData(currencyId);
        }

        private async void LoadData(string currencyId)
        {
            Tickers.AddRange(await _coinGeckoApiService.GetTickersByCurrencyId(currencyId));
        }
    }
}
