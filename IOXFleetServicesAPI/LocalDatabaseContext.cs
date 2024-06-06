using IOXFleetServicesAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;

namespace IOXFleetServicesAPI
{
    public class LocalDatabaseContext : DbContext
    {
        public LocalDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Quote>().ToTable("Quote");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");

        }

    }

}
