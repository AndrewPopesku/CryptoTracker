using CryptoTracker.Commands;
using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using CryptoTracker.Stores;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrencyViewModel : ViewModelBase
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string PriceUsd { get; set; }
        public string Change24Hr { get; set; }
        public string VolumeUsd24Hr { get; set; }
        public string MarketCapUsd { get; set; }

        public ICommand? DetailsCommand { get; }

        public CryptoCurrencyViewModel(CryptoCurrency cryptoCurrency)
        {
            Id = cryptoCurrency.Id;
            Symbol = cryptoCurrency.Symbol.ToUpper();
            Name = char.ToUpper(cryptoCurrency.Id[0]) + cryptoCurrency.Id.Substring(1);
            PriceUsd = cryptoCurrency.PriceUsd.ToString("C", new CultureInfo("en-us"));
            Change24Hr = cryptoCurrency.ChangePercent24Hr.ToString("+0.##%;-0.##%");
            VolumeUsd24Hr = cryptoCurrency.VolumeUsd24Hr.ToMoneyUsdStringFormat();
            MarketCapUsd = cryptoCurrency.MarketCapUsd.ToMoneyUsdStringFormat();
        }

        public CryptoCurrencyViewModel(
            CryptoCurrency cryptoCurrency,
            NavigationService<CryptoCurrencyDetailsViewModel> navigationService
            ) : this(cryptoCurrency)
        {
            DetailsCommand = new NavigateCommand<CryptoCurrencyDetailsViewModel>(navigationService);
        }
    }
}
