﻿using CryptoTracker.Models;
using CryptoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Helpers
{
    public static class ExtensionMethods
    {
        public static void AddRange(this ObservableCollection<CryptoCurrenciesListingItemViewModel> value, List<CryptoCurrenciesListingItemViewModel> list)
        {
            foreach(var crypto in list)
            {
                value.Add(crypto);
            }
        }

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
                return "$" + number.ToString();
            }
        }
    }
}
