using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrencyTickersViewModel
    {
        private readonly CoinGeckoApiService _coinGeckoApiService;

        public ObservableCollection<Ticker> Tickers { get; set; }

        public CryptoCurrencyTickersViewModel(CoinGeckoApiService coinGeckoApiService, string currencyId)
        {
            _coinGeckoApiService = coinGeckoApiService;

            Tickers = new ObservableCollection<Ticker>();
            LoadData(currencyId);
        }

        private async void LoadData(string currencyId)
        {
            Tickers.AddRange(await _coinGeckoApiService.GetTickersByCurrencyId(currencyId));
        }
    }
}
