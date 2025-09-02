// ExchangeRates.DAL/Providers/FreeCurrencyApiClient.cs
using System.Net.Http.Json;
using ExchangeRates.BL.Abstractions;
using ExchangeRates.BL.Models;
using ExchangeRates.DAL.Options;
using Microsoft.Extensions.Options;
namespace ExchangeRates.DAL.Providers
{


    public class FreeCurrencyApiClient : ICurrencyProvider
    {
        private readonly HttpClient _http;
        private readonly FreeCurrencyApiOptions _opt;

        public FreeCurrencyApiClient(HttpClient http, IOptions<FreeCurrencyApiOptions> opt)
        {
            _http = http;
            _opt = opt.Value;
        }

        public Task<IEnumerable<string>> GetSupportedAsync(CancellationToken ct = default)
            => Task.FromResult<IEnumerable<string>>(Enum.GetNames(typeof(SupportedCurrency)));

        public async Task<IEnumerable<RateItem>> GetRatesAsync(
            string @base,
            IEnumerable<string> quotes,
            CancellationToken ct = default)
        {
            var list = quotes.ToArray();
            if (list.Length == 0) return Array.Empty<RateItem>();

            var url = $"{_opt.BaseUrl}/latest?base_currency={@base}&currencies={string.Join(",", list)}&apikey={_opt.ApiKey}";
            var payload = await _http.GetFromJsonAsync<LatestResponse>(url, ct);

            return payload?.Data?.Select(kv => new RateItem(kv.Key, kv.Value))
                   ?? Enumerable.Empty<RateItem>();
        }

        private sealed class LatestResponse
        {
            public Dictionary<string, decimal>? Data { get; set; }
        }
    }
}