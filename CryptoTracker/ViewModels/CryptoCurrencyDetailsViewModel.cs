using CryptoTracker.Commands;
using CryptoTracker.Services;
using CryptoTracker.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using CryptoTracker.Models;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrencyDetailsViewModel : ViewModelBase
    {
        private readonly CapCoinService _capCoinService;

        private CryptoCurrencyViewModel _currency;
        public CryptoCurrencyViewModel Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public CryptoCurrencyHistoryViewModel HistoryViewModel { get; set; }

        public ICommand BackCommand { get; }

        public CryptoCurrencyDetailsViewModel(
            NavigationService<CryptoCurrenciesListingViewModel> navigationService,
            CapCoinService capCoinService,
            string currencyId
        )
        {
            _capCoinService = capCoinService;
            BackCommand = new NavigateCommand<CryptoCurrenciesListingViewModel>(navigationService);
            HistoryViewModel = new CryptoCurrencyHistoryViewModel(capCoinService, currencyId);
            LoadData(currencyId);
        }

        private async Task LoadData(string? currencyId)
        {
            if (currencyId != null)
            {
                Currency = (await _capCoinService.GetCryptoCurrencyById(currencyId))
                    .ToCryptoCurrencyViewMode();
            }
        }
    }
}
