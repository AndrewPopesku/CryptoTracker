using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Models
{
    public class CryptoCurrencyHistory
    {
        public string CurrencyId { get; set; }
        public List<PriceHistoryEntry> PriceHistory { get; set; }

        public CryptoCurrencyHistory(string currencyId, List<PriceHistoryEntry> prices)
        {
            CurrencyId = currencyId;
            PriceHistory = prices;
        }
    }
}
