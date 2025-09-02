using ExchangeRates.BL.Models;

public interface ICurrencyProvider
{
    Task<IEnumerable<string>> GetSupportedAsync(CancellationToken ct = default);

    Task<IEnumerable<RateItem>> GetRatesAsync(
        string @base,
        IEnumerable<string> quotes,
        CancellationToken ct = default);
}
