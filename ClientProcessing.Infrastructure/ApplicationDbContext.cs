using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProcessing.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientProcessing.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(x => x.Addresses)
                .WithOne()
                .HasForeignKey(x => x.ClientId);

        }
    }
}
