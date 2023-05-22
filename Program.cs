using Microsoft.Data.SqlClient;
using WatchStock.Repositories.StockRepository;
using WatchStock.RepositoryContracts.StockRepositoryContract;
using WatchStock.ServiceContracts.StockContract;
using WatchStock.Services.StockServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICurrentValue, CurrentValueService>();
builder.Services.AddTransient<ICurrentValueRepository, CurrentValueRepository>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<SqlConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("stockDBConfig")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
