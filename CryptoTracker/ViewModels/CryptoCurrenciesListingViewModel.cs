using CryptoTracker.Helpers;
using CryptoTracker.Services;
using CryptoTracker.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrenciesListingViewModel : ViewModelBase
    {
        private readonly CapCoinService _capCoinService;
        private ObservableCollection<CryptoCurrencyViewModel> _currencies;

        public ObservableCollection<CryptoCurrencyViewModel> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        public CryptoCurrenciesListingViewModel(
            CapCoinService capCoinService, 
            NavigationService<CryptoCurrencyDetailsViewModel> navigationService)
        {
            _capCoinService = capCoinService;
            _currencies = new ObservableCollection<CryptoCurrencyViewModel>();
            InitializeData(navigationService);
        }

        private async void InitializeData(NavigationService<CryptoCurrencyDetailsViewModel> navigationService)
        {
            var cryptoModelList = await _capCoinService.GetCryptoCurrencies(10);
            Currencies.AddRange(cryptoModelList, navigationService);
        }
    }
}
