using Microsoft.AspNetCore.Mvc;
using ExchangeRates.BL.Abstractions;
using ExchangeRates.BL.Models;

namespace YourNamespace.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrenciesController : ControllerBase
{
    private readonly IExchangeRateService _svc;
    public CurrenciesController(IExchangeRateService svc) => _svc = svc;

    [HttpGet] // GET /api/currencies
    public async Task<ActionResult<CurrenciesResponse>> Get(CancellationToken ct)
        => Ok(await _svc.GetCurrenciesAsync(ct));
}
