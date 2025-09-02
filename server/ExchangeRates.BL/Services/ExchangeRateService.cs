
using ExchangeRates.BL.Abstractions;
using ExchangeRates.BL.Models;
namespace ExchangeRates.BL.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ICurrencyProvider _provider;


        private static readonly SupportedCurrency[] Supported =
            (SupportedCurrency[])Enum.GetValues(typeof(SupportedCurrency));

        public ExchangeRateService(ICurrencyProvider provider) => _provider = provider;

        public Task<CurrenciesResponse> GetCurrenciesAsync(CancellationToken ct = default)
        {
            var names = Enum.GetNames(typeof(SupportedCurrency));
            return Task.FromResult(new CurrenciesResponse(names));
        }

        public async Task<RatesResponse> GetRatesAsync(string baseCurrency, CancellationToken ct = default)
        {
            if (!Enum.TryParse<SupportedCurrency>(baseCurrency, true, out var parsed))
                throw new ArgumentException($"Unsupported base currency: {baseCurrency}");

            var @base = parsed.ToString();
            var quotes = Supported.Where(c => c != parsed).Select(c => c.ToString());

            var items = await _provider.GetRatesAsync(@base, quotes, ct);
            return new RatesResponse(@base, items);
        }
    }
}
