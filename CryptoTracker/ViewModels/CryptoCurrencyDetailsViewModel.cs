using CryptoTracker.Commands;
using CryptoTracker.Services;
using CryptoTracker.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using CryptoTracker.Models;
using CryptoTracker.Exceptions;
using System.Windows.Navigation;
using System;

namespace CryptoTracker.ViewModels
{
    /// <summary>
    /// Represents a ViewModel responsible for displaying details of a specific cryptocurrency.
    /// </summary>
    public class CryptoCurrencyDetailsViewModel : ViewModelBase
    {
        private readonly NavigationService<CryptoCurrenciesListingViewModel> _navigationService;
        private readonly CapCoinService _capCoinService;
        private readonly CoinGeckoService _coinGeckoApiService;
        private CryptoCurrencyViewModel? _currency;

        /// <summary>
        /// Gets or sets the cryptocurrency details to display.
        /// </summary>
        public CryptoCurrencyViewModel? Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        private CryptoCurrencyHistoryViewModel? _historyViewModel;
        /// <summary>
        /// Gets or sets the cryptocurrency history to display.
        /// </summary>
        public CryptoCurrencyHistoryViewModel? HistoryViewModel
        {
            get => _historyViewModel;
            set
            {
                _historyViewModel = value;
                OnPropertyChanged(nameof(HistoryViewModel));
            }
        }

        private CryptoCurrencyTickersViewModel? _tickersViewModel;
        /// <summary>
        /// Gets or sets the cryptocurrency tickers to display.
        /// </summary>
        public CryptoCurrencyTickersViewModel? TickersViewModel
        {
            get => _tickersViewModel;
            set
            {
                _tickersViewModel = value;
                OnPropertyChanged(nameof(TickersViewModel));
            }
        }

        /// <summary>
        /// Gets the command to navigate back.
        /// </summary>
        public ICommand BackCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrencyDetailsViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">An instance of NavigationService for navigation purposes.</param>
        /// <param name="capCoinService">An instance of CapCoinService to retrieve cryptocurrency data.</param>
        /// <param name="coinGeckoApiService">An instance of CoinGeckoService for additional cryptocurrency data.</param>
        public CryptoCurrencyDetailsViewModel(
            NavigationService<CryptoCurrenciesListingViewModel> navigationService,
            CapCoinService capCoinService,
            CoinGeckoService coinGeckoApiService
        )
        {
            _navigationService = navigationService;
            _capCoinService = capCoinService;
            _coinGeckoApiService = coinGeckoApiService;
            BackCommand = new NavigateCommand(navigationService);
        }

        /// <summary>
        /// Loads data for a specific cryptocurrency using its ID.
        /// </summary>
        /// <param name="currencyId">The ID of the cryptocurrency to load data for.</param>
        /// <returns>A task representing the asynchronous data loading operation.</returns>
        internal async Task LoadData(string? currencyId)
        {
            if (currencyId != null)
            {
                try
                {
                    Currency = (await _capCoinService.GetCryptoCurrencyById(currencyId))
                        .ToCryptoCurrencyViewModel();
                    HistoryViewModel = new CryptoCurrencyHistoryViewModel(_capCoinService, currencyId);
                    TickersViewModel = new CryptoCurrencyTickersViewModel(_coinGeckoApiService, currencyId);
                }
                catch (FetchDataException ex)
                {
                    _navigationService.NavigateToErrorView(ex.Message, currencyId);
                }
                catch (Exception)
                {
                    _navigationService.NavigateToErrorView($"Something went wrong while loading '{currencyId}' currency", currencyId);
                }
            }
        }
    }
}
