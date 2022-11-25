using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Infrastructure;

public class RepositoryContext:DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options){}

    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Receipt
        modelBuilder.Entity<Receipt>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Receipt>().HasKey(e => e.Id);
        modelBuilder.Entity<Receipt>().HasOne(e => e.Order);

        
        //OrderItem
        modelBuilder.Entity<OrderItem>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<OrderItem>().HasKey(e => e.Id);
        modelBuilder.Entity<OrderItem>().HasOne(e => e.Order);
        
        //Order
        modelBuilder.Entity<Order>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Order>().HasKey(e => e.Id);
        modelBuilder.Entity<Order>().HasOne(e => e.OrderStatus);
        
        //OrderStatus
        modelBuilder.Entity<OrderStatus>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<OrderStatus>().HasKey(e => e.Id);


        
    }
}