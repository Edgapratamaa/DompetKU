using Microsoft.EntityFrameworkCore;

namespace DompetKU.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}