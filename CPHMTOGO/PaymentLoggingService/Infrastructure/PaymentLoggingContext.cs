using Microsoft.EntityFrameworkCore;
using PaymentLoggingService.Domain;

namespace PaymentLoggingService.Infrastructure;

public class PaymentLoggingContext : DbContext
{
    public PaymentLoggingContext(DbContextOptions<PaymentLoggingContext> options):base(options){}

    public DbSet<PaymentLogging> PaymentLoggings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //PaymentLogging
        modelBuilder.Entity<PaymentLogging>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<PaymentLogging>().HasKey(e=>e.Id);

    }
}