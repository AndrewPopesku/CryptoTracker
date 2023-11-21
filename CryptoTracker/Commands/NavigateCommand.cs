using CryptoTracker.Services;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using System;
using System.Threading.Tasks;

namespace CryptoTracker.Commands
{
    /// <summary>
    /// Represents a command used to navigate to different views or pages within the application.
    /// </summary>
    public class NavigateCommand : AsyncCommandBase
    {
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateCommand"/> class with the specified navigation service.
        /// </summary>
        /// <param name="navigationService">The navigation service used to perform navigation.</param>
        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        /// <summary>
        /// Executes the command asynchronously to navigate to a specific view or page.
        /// </summary>
        /// <param name="parameter">The parameter used for navigation (e.g., target view or page).</param>
        public override async Task ExecuteAsync(object? parameter)
        {
            await _navigationService.Navigate(parameter);
        }
    }
}
