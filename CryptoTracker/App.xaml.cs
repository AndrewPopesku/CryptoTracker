using CryptoTracker.Services;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly CapCoinService _capCoinService;

        public App()
        {
            _navigationStore = new NavigationStore();
            _capCoinService = new CapCoinService();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private CryptoCurrenciesListingViewModel CreateListingViewModel()
        {
            return new CryptoCurrenciesListingViewModel(_capCoinService, _navigationStore, CreateDetailsViewModel);
        }

        private CryptoCurrencyDetailsViewModel CreateDetailsViewModel()
        {
            return new CryptoCurrencyDetailsViewModel(_navigationStore, CreateListingViewModel);
        }
    }
}
