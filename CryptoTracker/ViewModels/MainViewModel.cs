using CryptoTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CapCoinService capCoinService = new CapCoinService();
            CurrentViewModel = new CryptoCurrenciesListingViewModel(capCoinService);
        }
    }
}
