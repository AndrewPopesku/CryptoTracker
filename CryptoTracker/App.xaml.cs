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
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(new CapCoinService("https://api.coincap.io/v2/"));
                    services.AddSingleton<NavigationStore>();

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

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var navigationService = _host.Services.GetRequiredService<NavigationService<CryptoCurrenciesListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
