using CryptoTracker.Commands;
using CryptoTracker.Exceptions;
using CryptoTracker.Helpers;
using CryptoTracker.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrenciesListingViewModel : ViewModelBase
    {
        private readonly CapCoinService _capCoinService;
        private readonly NavigationService<CryptoCurrencyDetailsViewModel> _navigationService;
        private ObservableCollection<CryptoCurrencyViewModel>? _currencies;

        public ObservableCollection<CryptoCurrencyViewModel> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ICommand SearchCommand { get; }

        public CryptoCurrenciesListingViewModel(
            CapCoinService capCoinService, 
            NavigationService<CryptoCurrencyDetailsViewModel> navigationService
            )
        {
            _capCoinService = capCoinService;
            _navigationService = navigationService;
            _currencies = new ObservableCollection<CryptoCurrencyViewModel>();

            InitializeData();
            SearchCommand = new SearchCommand(LoadFilteredData);
        }

        private async void InitializeData()
        {
            try
            {
                var cryptoModelList = await _capCoinService.GetTopCryptoCurrencies(10);
                Currencies.AddRange(cryptoModelList, _navigationService);
            }
            catch (FetchDataException ex)
            {
                _navigationService.NavigateToErrorView(ex.Message);
            }
            catch (Exception)
            {
                _navigationService.NavigateToErrorView("Something went wrong while loading cryptocurrenies");
            }
        }

        private async Task LoadFilteredData()
        {
            if (string.IsNullOrWhiteSpace(_searchText))
            {
                InitializeData();
            }
            else
            {
                try
                {
                    var cryptoModelList = await _capCoinService.GetCryptoCurrenciesByNameOrSymbol(_searchText);
                    _currencies.Clear();
                    Currencies.AddRange(cryptoModelList, _navigationService);
                }
                catch (FetchDataException ex)
                {
                    _navigationService.NavigateToErrorView(ex.Message);
                }
                catch (Exception)
                {
                    _navigationService.NavigateToErrorView("Something went wrong while loading cryptocurrenies");
                }
            }
        }
    }
}
