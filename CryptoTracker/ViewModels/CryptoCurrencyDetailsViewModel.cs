using CryptoTracker.Commands;
using CryptoTracker.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrencyDetailsViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }
            
        public CryptoCurrencyDetailsViewModel(
            NavigationStore navigationStore,
            Func<CryptoCurrenciesListingViewModel> listingViewModel
            )
        {
            BackCommand = new NavigateCommand(navigationStore, listingViewModel);
        }
    }
}
