using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.Run();

/*
using Yahoo.Finance;
var hdp = new HistoricalDataProvider();

hdp.DownloadHistoricalDataAsync("MSFT", new DateTime(2010, 1, 1), new DateTime(2010, 1, 31)).Wait();

if (hdp.DownloadResult == HistoricalDataDownloadResult.Successful)
{
    var result = hdp.HistoricalData;

    foreach (var item in result)
    {
        Console.WriteLine($"{item.Date} {item.Open} {item.High} {item.Low} {item.Close} {item.Volume}");
    }
}
*/