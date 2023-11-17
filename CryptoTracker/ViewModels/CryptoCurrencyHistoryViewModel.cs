using CryptoTracker.Helpers;
using CryptoTracker.Models;
using CryptoTracker.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTracker.ViewModels
{
    public class CryptoCurrencyHistoryViewModel : ViewModelBase
    {
        private readonly CapCoinService _capCoinService;
        private SeriesCollection _priceSeries;
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
        public Func<double, string> YAxisLabelFormatter
        {
            get => _yAxisLabelFormatter;
            set
            {
                _yAxisLabelFormatter = value;
                OnPropertyChanged(nameof(YAxisLabelFormatter));
            }
        }

        public CryptoCurrencyHistoryViewModel(CapCoinService capCoinService, string currencyId)
        {
            _capCoinService = capCoinService;
            InitializeChart(currencyId);
        }

        private async void InitializeChart(string currencyId)
        {
            List<PriceHistoryEntry> priceHistoryList = await _capCoinService.GetCryptoCurrencyHistoryById(currencyId);

            ChartValues<ObservablePoint> chartValues = new ChartValues<ObservablePoint>();
            foreach (var entry in priceHistoryList)
            {
                chartValues.Add(new ObservablePoint
                {
                    X = chartValues.Count, // Assign an index as X-value
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

            // Assigning labels directly as strings
            var labels = priceHistoryList.Select(entry => entry.Date.AddMicroseconds(entry.Time)
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
