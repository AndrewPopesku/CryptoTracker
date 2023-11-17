using CryptoTracker.Services;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using System;
using System.Threading.Tasks;

namespace CryptoTracker.Commands
{
    public class NavigateCommand<TViewModel> : AsyncCommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _navigationService.Navigate(parameter);
        }
    }
}
