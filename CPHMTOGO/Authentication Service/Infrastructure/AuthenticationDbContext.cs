using AuthenticationService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure;

public class AuthenticationDbContext : DbContext 
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> dbContextOptions):base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoginInfo>().Property(e => e.Id).ValueGeneratedOnAdd();
    }

    public DbSet<LoginInfo> Infos { get; set; }
    
}