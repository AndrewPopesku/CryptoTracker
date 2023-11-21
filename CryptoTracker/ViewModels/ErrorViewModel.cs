using CryptoTracker.Commands;
using CryptoTracker.Services;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    /// <summary>
    /// Represents a ViewModel to display an error message and handle retries or navigation.
    /// </summary>
    public class ErrorViewModel : ViewModelBase
    {
        private string? _errorMessage;

        /// <summary>
        /// Gets or sets the error message displayed by the ViewModel.
        /// </summary>
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        /// <summary>
        /// Gets the command to retry or navigate on error.
        /// </summary>
        public ICommand? RetryCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorViewModel"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed.</param>
        /// <param name="navigationService">The navigation service for handling retries or navigation.</param>
        /// <param name="parameter">Additional parameter if needed for navigation.</param>
        public ErrorViewModel(string errorMessage, INavigationService navigationService, object? parameter)
        {
            ErrorMessage = errorMessage;
            RetryCommand = new NavigateCommand(navigationService);
        }
    }
}
