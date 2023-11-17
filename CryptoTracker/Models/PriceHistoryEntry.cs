using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Models
{
    public class PriceHistoryEntry
    {
        public double PriceUsd { get; set; }
        public long Time { get; set; }
        public string CirculatingSupply { get; set; }
        public DateTime Date { get; set; }
    }
}
