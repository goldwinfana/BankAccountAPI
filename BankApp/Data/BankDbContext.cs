using BankApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
namespace BankApp.Data
{
    public class BankDbContext : DbContext
    {
       
        private static IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>()
                .HasMany(a => a.Accounts)
                .WithOne(b => b.AccountHolder)
                .HasForeignKey(b => b.AccountHolderId);

            modelBuilder.Entity<Accounts>()
                .Property(a => a.AvailableBalance)
                .HasPrecision(18, 2); // specify precision and scale here

            base.OnModelCreating(modelBuilder);
        }
      
    }
    

}
