using CryptoTracker.Commands;
using CryptoTracker.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

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
        }
    }
}
