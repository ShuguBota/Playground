using Algorithmic_Trading.Database;
using Algorithmic_Trading.Repositories;
using Algorithmic_Trading.Services;
using Microsoft.EntityFrameworkCore;
using Yahoo.Finance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var x = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStockDataRepository, StockDataRepository>();
builder.Services.AddScoped<IDateTriedRepository, DateTriedRepository>();

builder.Services.AddScoped<IStockDataService, StockDataService>();

builder.Services.AddSingleton<HistoricalDataProvider>();
builder.Services.AddSingleton<IYFinanceService, YFinanceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();