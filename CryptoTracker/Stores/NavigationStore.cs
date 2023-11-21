using CryptoTracker.ViewModels;
using System;

namespace CryptoTracker.Stores
{
    /// <summary>
    /// Represents a store managing the current ViewModel used for navigation within the CryptoTracker application.
    /// </summary>
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        /// <summary>
        /// Gets or sets the current ViewModel used for navigation.
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChange();
            }
        }

        /// <summary>
        /// Event raised when the current ViewModel changes.
        /// </summary>
        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
