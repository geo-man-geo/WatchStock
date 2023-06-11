using Microsoft.Data.SqlClient;
using NLog.Extensions.Logging;
using WatchStock.ServiceContracts.StockContract;
using WatchStock.Services.StockServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<SqlConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("stockDBConfig")));
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddNLog();
});
var appInsightsConnectionString = builder.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
builder.Services.AddApplicationInsightsTelemetryWorkerService(options =>
{
    options.ConnectionString = appInsightsConnectionString;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.Logger.LogDebug("debug-message");
app.Logger.LogInformation("information-message");
app.Logger.LogCritical("critical-message");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
