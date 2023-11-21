using CryptoTracker.Services;
using CryptoTracker.Stores;
using CryptoTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace CryptoTracker
{
    /// <summary>
    /// Interaction logic for the application.
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // Registering services and view models in the dependency injection container.
                    services.AddSingleton(new CapCoinService("https://api.coincap.io/v2/"));
                    services.AddSingleton(new CoinGeckoService("https://api.coingecko.com/api/v3/"));
                    services.AddSingleton<NavigationStore>();

                    // Registration of ViewModels and associated services.
                    services.AddTransient<CryptoCurrenciesListingViewModel>();
                    services.AddSingleton<Func<CryptoCurrenciesListingViewModel>>(s =>
                        () => s.GetRequiredService<CryptoCurrenciesListingViewModel>());
                    services.AddSingleton<NavigationService<CryptoCurrenciesListingViewModel>>();

                    services.AddTransient<CryptoCurrencyDetailsViewModel>();
                    services.AddSingleton<Func<CryptoCurrencyDetailsViewModel>>(s =>
                        () => s.GetRequiredService<CryptoCurrencyDetailsViewModel>());
                    services.AddSingleton<NavigationService<CryptoCurrencyDetailsViewModel>>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        /// <summary>
        /// Handles the application startup event.
        /// </summary>
        /// <param name="e">The startup event arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            // Navigation initialization on startup.
            var navigationService = _host.Services.GetRequiredService<NavigationService<CryptoCurrenciesListingViewModel>>();
            navigationService.Navigate(null);

            // Displaying the main window.
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Handles the application exit event.
        /// </summary>
        /// <param name="e">The exit event arguments.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Disposing the host on application exit.
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
