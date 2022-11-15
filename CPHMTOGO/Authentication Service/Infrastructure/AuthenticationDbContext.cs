using AuthenticationService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure;

public class AuthenticationDbContext : DbContext 
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> dbContextOptions):base(dbContextOptions)
    {
    }

    public DbSet<LoginInfo> Infos { get; set; }
    
}