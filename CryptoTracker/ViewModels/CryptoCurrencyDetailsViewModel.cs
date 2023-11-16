using CryptoTracker.Commands;
using CryptoTracker.Services;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrencyDetailsViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }
            
        public CryptoCurrencyDetailsViewModel(
            NavigationService<CryptoCurrenciesListingViewModel> navigationService
            )
        {
            BackCommand = new NavigateCommand<CryptoCurrenciesListingViewModel>(navigationService);
        }
    }
}
