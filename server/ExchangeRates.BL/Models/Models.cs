using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.BL.Models;

public record RateItem(string Currency, decimal Rate);
public record RatesResponse(string Base, IEnumerable<RateItem> Rates);
public record CurrenciesResponse(IEnumerable<string> Currencies);