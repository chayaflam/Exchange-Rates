using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExchangeRates.BL.Models;

namespace ExchangeRates.BL.Abstractions;

public interface IExchangeRateService
{
    Task<CurrenciesResponse> GetCurrenciesAsync(CancellationToken ct = default);
    Task<RatesResponse> GetRatesAsync(string baseCurrency, CancellationToken ct = default);
}

