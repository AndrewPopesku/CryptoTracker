using CryptoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChange();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
