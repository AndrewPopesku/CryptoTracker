using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoTracker.ViewModels
{
    /// <summary>
    /// Represents a ViewModel responsible for displaying the price history of a cryptocurrency.
    /// </summary>
    public class CryptoCurrencyHistoryViewModel : ViewModelBase
    {
        private readonly CapCoinService _capCoinService;
        private SeriesCollection _priceSeries;

        /// <summary>
        /// Gets or sets the collection of series representing the price history.
        /// </summary>
        public SeriesCollection PriceSeries
        {
            get => _priceSeries;
            set
            {
                _priceSeries = value;
                OnPropertyChanged(nameof(PriceSeries));
            }
        }

        private Func<double, string> _formatter;

        /// <summary>
        /// Gets or sets the formatter for chart labels.
        /// </summary>
        public Func<double, string> Formatter
        {
            get => _formatter;
            set
            {
                _formatter = value;
                OnPropertyChanged(nameof(Formatter));
            }
        }

        private Func<double, string> _yAxisLabelFormatter;
        /// <summary>
        /// Gets or sets the formatter for Y-axis labels.
        /// </summary>
        public Func<double, string> YAxisLabelFormatter
        {
            get => _yAxisLabelFormatter;
            set
            {
                _yAxisLabelFormatter = value;
                OnPropertyChanged(nameof(YAxisLabelFormatter));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrencyHistoryViewModel"/> class.
        /// </summary>
        /// <param name="capCoinService">An instance of CapCoinService to retrieve cryptocurrency data.</param>
        /// <param name="currencyId">An id of cryptocurrency</param>
        public CryptoCurrencyHistoryViewModel(CapCoinService capCoinService, string currencyId)
        {
            _capCoinService = capCoinService;
            InitializeChart(currencyId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCurrencyHistoryViewModel"/> class.
        /// </summary>
        /// <param name="capCoinService">An instance of CapCoinService to retrieve cryptocurrency history data.</param>
        /// <param name="currencyId">The ID of the cryptocurrency to display history for.</param>
        private async void InitializeChart(string currencyId)
        {
            List<PriceHistoryEntry> priceHistoryList = await _capCoinService.GetCryptoCurrencyHistoryById(currencyId);

            ChartValues<ObservablePoint> chartValues = new ChartValues<ObservablePoint>();
            foreach (var entry in priceHistoryList)
            {
                chartValues.Add(new ObservablePoint
                {
                    X = chartValues.Count,
                    Y = Convert.ToDouble(entry.PriceUsd)
                });
            }

            PriceSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = chartValues,
                    PointGeometry = null,
                }
            };

            var labels = priceHistoryList.Select(entry => entry.Date
                .ToString("MM/dd/yyyy HH:mm")).ToArray();

            YAxisLabelFormatter = value => value.ToMoneyUsdStringFormat();
            Formatter = value =>
            {
                var index = (int)value;
                return index >= 0 && index < labels.Length ? labels[index] : "";
            };
        }
    }
}
