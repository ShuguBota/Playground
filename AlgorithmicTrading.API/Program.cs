using Microsoft.EntityFrameworkCore;
using AlgorithmicTrading.Data.Database;
using AlgorithmicTrading.Data.Repositories;
using AlgorithmicTrading.Logic.Services;
using Yahoo.Finance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var x = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStockDataRepository, StockDataRepository>();
builder.Services.AddScoped<IDateTriedRepository, DateTriedRepository>();

builder.Services.AddScoped<IStockDataService, StockDataService>();
builder.Services.AddScoped<ICsvService, CsvService>();

builder.Services.AddSingleton<HistoricalDataProvider>();
builder.Services.AddSingleton<IYFinanceService, YFinanceService>();

builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();