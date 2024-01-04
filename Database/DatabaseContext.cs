using Algorithmic_Trading.Models;
using Microsoft.EntityFrameworkCore;

namespace Algorithmic_Trading.Database;

public class DatabaseContext : DbContext
{
    public DbSet<StockData> Stocks { get; set; }
    public DbSet<DateTried> DatesTried { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockData>()
            .HasKey(stock => new { stock.Ticker, stock.Date });

        modelBuilder.Entity<DateTried>()
            .HasKey(date => new { date.Ticker, date.Date});
    }
}