using Microsoft.EntityFrameworkCore;
using TopUpService.Models;

namespace TopUpService.Providers
{
    public class TopUpDbContext : DbContext
    {
        public TopUpDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Beneficiary>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
            modelBuilder
                .Entity<UserTransaction>()
                .Property(e => e.TransactionStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (TransactionStatus)Enum.Parse(typeof(TransactionStatus), v));
        }

        public DbSet<Beneficiary> Beneficiary { get; set; }
        public DbSet<TopUpOption> TopUpOption { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserTransaction> UserTransaction { get; set; }

    }
}
