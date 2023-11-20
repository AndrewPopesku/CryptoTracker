using CryptoTracker.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoTracker.Models
{
    public class Ticker
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public Market Market { get; set; }
        [JsonProperty("last")]
        public double Price { get; set; }
        public double Volume { get; set; }
        [JsonProperty("trade_url")]
        public string TradeUrl { get; set; }

        public ICommand HyperLinkCommand { get; set; } = new HyperLinkCommand();
    }
}
