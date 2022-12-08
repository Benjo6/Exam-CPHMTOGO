using AddressService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AddressService.Infrastructure;

public class AddressDbContext : DbContext
{
    public AddressDbContext(DbContextOptions<AddressDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoginInfo>().Property(e => e.Id).ValueGeneratedOnAdd();
    }

    public DbSet<LoginInfo> Infos { get; set; }

}