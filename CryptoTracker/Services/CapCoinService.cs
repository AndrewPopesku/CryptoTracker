using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    internal class CapCoinService
    {
        private HttpClient _httpClient;
        private static string _baseUrl = "api.coincap.io/v2/";

        public CapCoinService()
        {
            _httpClient = new HttpClient();
        }
    }
}