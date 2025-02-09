using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure;
public class CRMDbContext : DbContext
{
    public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Address> Addresses { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Customer>()
          .HasMany(c => c.Branches)
          .WithOne(b => b.Customer)
          .HasForeignKey(b => b.CustomerId);

        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Addresses)
            .WithOne(a => a.Branch)
            .HasForeignKey(a => a.BranchId);

        // Bridge Table User <--> Role


        base.OnModelCreating(modelBuilder);


    }


}


