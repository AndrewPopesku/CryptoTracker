using CryptoTracker.Commands;
using CryptoTracker.Services;
using System.Windows.Input;

namespace CryptoTracker.ViewModels
{
    public class ErrorViewModel : ViewModelBase
    {
        private string? _errorMessage;

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand? RetryCommand { get; }

        public ErrorViewModel(string errorMessage, INavigationService navigationService, object? parameter)
        {
            ErrorMessage = errorMessage;
            RetryCommand = new NavigateCommand(navigationService);
        }
    }
}
