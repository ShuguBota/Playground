using Algorithmic_Trading.Models;
using Microsoft.AspNetCore.Mvc;
using Yahoo.Finance;

namespace Algorithmic_Trading.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockDataController : ControllerBase
{
    private readonly IStockDataService _stockDataService;

    public StockDataController(IStockDataService stockDataService)
    {
        _stockDataService = stockDataService;
    }

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
}