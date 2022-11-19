using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Infrastructure;

public class PostgresContext:DbContext
{
    public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();

    public PostgresContext(DbContextOptions<PostgresContext> options):base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Receipt
        modelBuilder.Entity<Receipt>().Property(e => e.Id).ValueGeneratedOnAdd();
        
        
        //OrderItem
        modelBuilder.Entity<OrderItem>().Property(e => e.Id).ValueGeneratedOnAdd();
        
        //Order
        modelBuilder.Entity<Order>().Property(e => e.Id).ValueGeneratedOnAdd();
        
        //OrderStatus
        modelBuilder.Entity<OrderStatus>().Property(e => e.Id).ValueGeneratedOnAdd();

        
    }
}