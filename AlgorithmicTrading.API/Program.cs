using Microsoft.EntityFrameworkCore;
using AlgorithmicTrading.Logic.Services;
using Yahoo.Finance;
using AlgorithmicTrading.Data.Database;
using Algorithmic_Trading.Data;
using AlgorithmicTrading.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

DataExtension.AddDataServices(builder.Services);
LogicExtension.AddLogicServices(builder.Services);

builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();