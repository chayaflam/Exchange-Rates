using ExchangeRates.BL.Abstractions;
using ExchangeRates.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatesController : ControllerBase
{
    private readonly IExchangeRateService _svc;
    public RatesController(IExchangeRateService svc) => _svc = svc;

    [HttpGet]
    public Task<RatesResponse> Get([FromQuery] string baseCurrency, CancellationToken ct)
       => _svc.GetRatesAsync(baseCurrency, ct);
}