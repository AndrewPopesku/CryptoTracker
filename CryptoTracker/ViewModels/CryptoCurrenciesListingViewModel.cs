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
    /// <summary>
    /// Represents a ViewModel responsible for listing and searching cryptocurrencies.
    /// </summary>
    public class CryptoCurrenciesListingViewModel : ViewModelBase
    {
        private readonly CapCoinService _capCoinService;
        private readonly NavigationService<CryptoCurrencyDetailsViewModel> _navigationService;
        private ObservableCollection<CryptoCurrencyViewModel>? _currencies;

        /// <summary>
        /// Gets or sets the collection of cryptocurrencies.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the search text for filtering cryptocurrencies.
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        /// <summary>
        /// Gets the command to perform a search based on the provided text.
        /// </summary>
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrenciesListingViewModel"/> class.
        /// </summary>
        /// <param name="capCoinService">An instance of CapCoinService to retrieve cryptocurrency data.</param>
        /// <param name="navigationService">An instance of NavigationService for navigation purposes.</param>
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

        /// <summary>
        /// Initializes the collection of cryptocurrencies.
        /// </summary>
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

        /// <summary>
        /// Sets the collection of filtered cryptocurrencies
        /// </summary>
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
