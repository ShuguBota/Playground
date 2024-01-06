using Algorithmic_Trading.Models;
using Microsoft.EntityFrameworkCore;

namespace Algorithmic_Trading.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<StockData> Stocks { get; set; }
    public DbSet<DateTried> DatesTried { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockData>()
            .HasKey(stock => new { stock.Ticker, stock.Date });

        modelBuilder.Entity<DateTried>()
            .HasKey(date => new { date.Ticker, date.Date});
    }
}