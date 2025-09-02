using ExchangeRates.BL.Abstractions;
using ExchangeRates.BL.Services;
using ExchangeRates.DAL.Options;
using ExchangeRates.DAL.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FreeCurrencyApiOptions>(opt =>
{
    builder.Configuration.GetSection("FreeCurrencyApi").Bind(opt);
    opt.ApiKey = builder.Configuration["FreeCurrencyApi:ApiKey"] ?? opt.ApiKey;
});

builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();   
builder.Services.AddHttpClient<ICurrencyProvider, FreeCurrencyApiClient>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string CorsPolicy = "Frontend";
builder.Services.AddCors(o => o.AddPolicy(CorsPolicy, p =>
    p.WithOrigins("http://localhost:5173") 
     .AllowAnyHeader()
     .AllowAnyMethod()
));

var app = builder.Build();

app.UseCors(CorsPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
