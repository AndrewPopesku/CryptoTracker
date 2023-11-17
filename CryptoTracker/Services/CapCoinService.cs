﻿using CryptoTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class CapCoinService
    {
        private HttpClient _httpClient;
        private string _baseUrl;

        public CapCoinService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        public async Task<List<CryptoCurrency>> GetTopCryptoCurrencies(int quantity)
        {
            var endpoint = "assets/";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrencies = JsonConvert.DeserializeObject<List<CryptoCurrency>>(jsonString);

            return cryptoCurrencies.OrderByDescending(crypto => crypto.MarketCapUsd).Take(quantity).ToList();
        }

        public async Task<CryptoCurrency> GetCryptoCurrencyById(string id)
        {
            var endpoint = "assets/" + id;
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoCurrency = JsonConvert.DeserializeObject<CryptoCurrency>(jsonString);

            return cryptoCurrency;
        }

        public async Task<List<PriceHistoryEntry>> GetCryptoCurrencyHistoryById(string id)
        {
            var endpoint = "assets/" + id + "/history?interval=d1";
            var jsonString = await GetJsonDataFromEndpoint(endpoint);

            var cryptoPriceHistoryEntries = JsonConvert.DeserializeObject<List<PriceHistoryEntry>>(jsonString);

            return cryptoPriceHistoryEntries.TakeLast(20).ToList();
        }

        private async Task<string> GetJsonDataFromEndpoint(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_baseUrl + endpoint);
                var rawJson = JObject.Parse(response);
                return rawJson["data"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data from endpoint '{endpoint}': {ex.Message}");
                return null;
            }
        }
    }
}