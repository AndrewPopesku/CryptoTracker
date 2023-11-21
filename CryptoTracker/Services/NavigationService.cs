using CryptoTracker.Commands;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using System;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    /// <summary>
    /// Provides navigation functionality for specific view models within the CryptoTracker application.
    /// </summary>
    /// <typeparam name="TViewModel">The type of ViewModel to navigate.</typeparam>
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        /// <summary>
        /// Gets or sets the currency ID associated with the navigation service.
        /// </summary>
        public string CurrencyId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService{TViewModel}"/> class.
        /// </summary>
        /// <param name="navigationStore">The store managing navigation state.</param>
        /// <param name="createViewModel">The function to create instances of the specified ViewModel.</param>
        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore ?? throw new ArgumentNullException(nameof(navigationStore));
            _createViewModel = createViewModel ?? throw new ArgumentNullException(nameof(createViewModel));
        }

        /// <summary>
        /// Navigates to the specified ViewModel asynchronously.
        /// </summary>
        /// <param name="parameter">The parameter passed during navigation.</param>
        /// <returns>A task representing the asynchronous navigation operation.</returns>
        public async Task Navigate(object? parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();

            if (parameter != null)
            {
                var detailsVM = _navigationStore.CurrentViewModel as CryptoCurrencyDetailsViewModel;
                await detailsVM.LoadData(parameter as string);
            }
        }

        /// <summary>
        /// Navigates to an error view with the specified error message and optional parameter.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed in the error view.</param>
        /// <param name="parameter">The optional parameter to be passed to the error view.</param>
        public void NavigateToErrorView(string errorMessage, object? parameter = null)
        {
            _navigationStore.CurrentViewModel = new ErrorViewModel(errorMessage, this, parameter);
        }
    }
}
