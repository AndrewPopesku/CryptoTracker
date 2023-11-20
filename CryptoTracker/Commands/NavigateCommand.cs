using CryptoTracker.Services;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using System;
using System.Threading.Tasks;

namespace CryptoTracker.Commands
{
    public class NavigateCommand : AsyncCommandBase
    {
        private readonly INavigationService _navigationService;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _navigationService.Navigate(parameter);
        }
    }
}
