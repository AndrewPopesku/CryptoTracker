using CryptoTracker.Commands;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CryptoTracker.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public string CurrencyId { get; set; }

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public async Task Navigate(object? parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
            if (parameter != null)
            {
                var detailsVM = _navigationStore.CurrentViewModel as CryptoCurrencyDetailsViewModel;
                await detailsVM.LoadData(parameter as string);
            }
        }

        public void NavigateToErrorView(string errorMessage, object? parameter = null)
        {
            _navigationStore.CurrentViewModel = new ErrorViewModel(errorMessage, this, parameter);
        }
    }
}
