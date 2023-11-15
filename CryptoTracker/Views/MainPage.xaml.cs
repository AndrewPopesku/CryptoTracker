using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using CryptoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoTracker.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<CryptoCurrenciesListingItemViewModel> Data { get; set; }  
            = new ObservableCollection<CryptoCurrenciesListingItemViewModel>();
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var service = new CapCoinService();
            var listingViewModel = new CryptoCurrenciesListingViewModel(service);
            var cryptos = await listingViewModel.GetData();
            Data.AddRange(cryptos);
        }
    }
}
