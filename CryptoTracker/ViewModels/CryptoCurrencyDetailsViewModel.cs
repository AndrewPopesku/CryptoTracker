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
    public class CryptoCurrencyDetailsViewModel : ViewModelBase
    {
        private readonly NavigationService<CryptoCurrenciesListingViewModel> _navigationService;
        private readonly CapCoinService _capCoinService;
        private CryptoCurrencyViewModel? _currency;
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
        public CryptoCurrencyHistoryViewModel? HistoryViewModel
        {
            get => _historyViewModel;
            set
            {
                _historyViewModel = value;
                OnPropertyChanged(nameof(HistoryViewModel));
            }
        }

        public ICommand BackCommand { get; }

        public CryptoCurrencyDetailsViewModel(
            NavigationService<CryptoCurrenciesListingViewModel> navigationService,
            CapCoinService capCoinService
        )
        {
            _navigationService = navigationService;
            _capCoinService = capCoinService;
            BackCommand = new NavigateCommand<CryptoCurrenciesListingViewModel>(navigationService);
        }

        internal async Task LoadData(string? currencyId)
        {
            if (currencyId != null)
            {
                try
                {
                    Currency = (await _capCoinService.GetCryptoCurrencyById(currencyId))
                        .ToCryptoCurrencyViewMode();
                    HistoryViewModel = new CryptoCurrencyHistoryViewModel(_capCoinService, currencyId);
                }
                catch (FetchDataException ex)
                {
                    _navigationService.NavigateToErrorView(ex.Message, currencyId);
                }
                catch (Exception ex)
                {
                    _navigationService.NavigateToErrorView($"Something went wrong while loading '{currencyId}' currency", currencyId);
                }
            }
        }
    }
}
