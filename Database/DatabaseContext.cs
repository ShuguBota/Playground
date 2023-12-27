using Algorithmic_Trading.Models;
using Microsoft.EntityFrameworkCore;

namespace Algorithmic_Trading.Database;

public class DatabaseContext : DbContext
{
    public DbSet<StockData> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockData>()
            .HasKey(stock => new { stock.Ticker, stock.Date });
    }
}