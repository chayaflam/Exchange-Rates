using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.DAL.Options
{
    public class FreeCurrencyApiOptions
    {
        public string BaseUrl { get; set; } = "https://api.freecurrencyapi.com/v1";
        public string ApiKey { get; set; } = string.Empty;
    }
}
