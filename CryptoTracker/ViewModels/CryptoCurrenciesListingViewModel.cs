using CryptoTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrenciesListingViewModel
    {
        private readonly CapCoinService capCoinService;

        public CryptoCurrenciesListingViewModel(CapCoinService capCoinService)
        {
            this.capCoinService = capCoinService;
        }

        public async Task<List<CryptoCurrenciesListingItemViewModel>> GetData()
        {
            var result = new List<CryptoCurrenciesListingItemViewModel>();

            var cryptoModelList = await capCoinService.GetCryptoCurrencies(10);
            foreach(var c in cryptoModelList)
            {
                result.Add(new CryptoCurrenciesListingItemViewModel(c));
            }

            return result;
        }
    }
}
