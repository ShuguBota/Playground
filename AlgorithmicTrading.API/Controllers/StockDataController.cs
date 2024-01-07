using System.Text;
using AlgorithmicTrading.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmicTrading.Logic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockDataController(IStockDataService stockDataService, ICsvService csvService) : ControllerBase
{
    private readonly IStockDataService _stockDataService = stockDataService;
    private readonly ICsvService _csvService = csvService;

    [HttpGet]
    public async Task<IActionResult> GetStockData(string ticker, DateTime startDate, DateTime endDate)
    {
        try{
            var data = await _stockDataService.GetStockData(ticker, startDate, endDate);

            return Ok(data);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }


    [HttpGet("csv")]
    public async Task<IActionResult> GetStockDataCsv(string ticker, DateTime startDate, DateTime endDate)
    {
        try
        {
            var data = await _stockDataService.GetStockData(ticker, startDate, endDate);
            var writer = _csvService.GetCSV(data);

            return File(Encoding.UTF8.GetBytes(writer), "text/csv", "data.csv");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}