using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<string, TViewModel> _createViewModel;

        public string CurrencyId { get; set; }

        public NavigationService(NavigationStore navigationStore, Func<string, TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(object? parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(parameter as string);
        }
    }
}
