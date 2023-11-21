using CryptoTracker.Stores;
using System;

namespace CryptoTracker.ViewModels
{
    /// <summary>
    /// Represents the main ViewModel responsible for managing the current view in the application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Gets the current ViewModel displayed in the application.
        /// </summary>
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="navigationStore">The navigation store managing view changes.</param>
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore ?? throw new ArgumentNullException(nameof(navigationStore));

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
