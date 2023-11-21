using CryptoTracker.Models;
using CryptoTracker.Services;
using CryptoTracker.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CryptoTracker.Helpers
{
    /// <summary>
    /// Contains extension methods for various data types used within the CryptoTracker application.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds a range of CryptoCurrencyViewModel instances to an ObservableCollection from a list of CryptoCurrency objects.
        /// </summary>
        /// <param name="value">The ObservableCollection to which the CryptoCurrencyViewModel instances will be added.</param>
        /// <param name="list">The list of CryptoCurrency objects.</param>
        /// <param name="navigationService">The navigation service used for CryptoCurrencyDetailsViewModel.</param>
        public static void AddRange(
            this ObservableCollection<CryptoCurrencyViewModel> value,
            List<CryptoCurrency> list,
            NavigationService<CryptoCurrencyDetailsViewModel> navigationService
            )
        {
            foreach (var crypto in list)
            {
                value.Add(new CryptoCurrencyViewModel(crypto, navigationService));
            }
        }

        /// <summary>
        /// Converts a CryptoCurrency object to a CryptoCurrencyViewModel instance.
        /// </summary>
        /// <param name="cryptoCurrency">The CryptoCurrency object to be converted.</param>
        /// <returns>A CryptoCurrencyViewModel instance.</returns>
        public static CryptoCurrencyViewModel ToCryptoCurrencyViewModel(this CryptoCurrency cryptoCurrency)
        {
            return new CryptoCurrencyViewModel(cryptoCurrency);
        }

        /// <summary>
        /// Adds a range of Ticker instances to an ObservableCollection from a list of Ticker objects.
        /// </summary>
        /// <param name="value">The ObservableCollection to which the Ticker instances will be added.</param>
        /// <param name="list">The list of Ticker objects.</param>
        public static void AddRange(
            this ObservableCollection<Ticker> value,
            List<Ticker> list
            )
        {
            foreach (var ticker in list)
            {
                value.Add(ticker);
            }
        }

        /// <summary>
        /// Converts a double value to a string formatted as money in USD.
        /// </summary>
        /// <param name="number">The double value to be formatted.</param>
        /// <returns>A string representing the number in money USD format.</returns>
        public static string ToMoneyUsdStringFormat(this double number)
        {
            if (number >= 1_000_000_000) // If the number is a billion or more
            {
                return "$" + (number / 1_000_000_000).ToString("0.##") + "B";
            }
            else if (number >= 1_000_000) // If the number is a million or more
            {
                return "$" + (number / 1_000_000).ToString("0.##") + "M";
            }
            else if (number >= 1_000) // If the number is a thousand or more
            {
                return "$" + (number / 1_000).ToString("0.##") + "K";
            }
            else // Otherwise, just show the number as is
            {
                return "$" + number.ToString("0.#####");
            }
        }
    }
}
