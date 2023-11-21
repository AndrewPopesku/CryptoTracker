using CryptoTracker.Commands;
using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using System;
using System.Globalization;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    /// <summary>
    /// Represents a ViewModel for displaying information about a cryptocurrency.
    /// </summary>
    public class CryptoCurrencyViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the unique identifier of the cryptocurrency.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the symbol of the cryptocurrency.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets the name of the cryptocurrency.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the price of the cryptocurrency in USD.
        /// </summary>
        public string PriceUsd { get; set; }

        /// <summary>
        /// Gets the change percentage of the cryptocurrency in the last 24 hours.
        /// </summary>
        public string Change24Hr { get; set; }

        /// <summary>
        /// Gets the volume of the cryptocurrency in USD for the last 24 hours.
        /// </summary>
        public string VolumeUsd24Hr { get; set; }

        /// <summary>
        /// Gets the market capitalization of the cryptocurrency in USD.
        /// </summary>
        public string MarketCapUsd { get; set; }

        /// <summary>
        /// Gets the command to navigate to details view for the cryptocurrency.
        /// </summary>
        public ICommand? DetailsCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrencyViewModel"/> class.
        /// </summary>
        /// <param name="cryptoCurrency">The cryptocurrency model containing data.</param>
        public CryptoCurrencyViewModel(CryptoCurrency cryptoCurrency)
        {
            Id = cryptoCurrency.Id;
            Symbol = cryptoCurrency.Symbol.ToUpper();
            Name = cryptoCurrency.Name;
            PriceUsd = cryptoCurrency.PriceUsd.ToString("C", new CultureInfo("en-us"));
            Change24Hr = cryptoCurrency.ChangePercent24Hr.ToString("+0.##%;-0.##%");
            VolumeUsd24Hr = cryptoCurrency.VolumeUsd24Hr.ToMoneyUsdStringFormat();
            MarketCapUsd = cryptoCurrency.MarketCapUsd.ToMoneyUsdStringFormat();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrencyViewModel"/> class with navigation.
        /// </summary>
        /// <param name="cryptoCurrency">The cryptocurrency model containing data.</param>
        /// <param name="navigationService">The navigation service for details view.</param>
        public CryptoCurrencyViewModel(
            CryptoCurrency cryptoCurrency,
            NavigationService<CryptoCurrencyDetailsViewModel> navigationService
            ) : this(cryptoCurrency)
        {
            DetailsCommand = new NavigateCommand(navigationService);
        }
    }
}
