using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using OrderService.Domain.EntityHelper;

namespace OrderService.Domain;
[Table("Order"),MessagePackObject(keyAsPropertyName:true)]

public class Order : EntityWithId<Guid>
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    
    [Column("address")] 
    public string Address { get; set; } = default!;

    [Column("customerId")] public string CustomerId { get; set; } = default!;

    [Column("employeeId")] public string EmployeeId { get; set; } = default!;
    
    [Column("restaurantId")] public string RestaurantId { get; set; }
    
    [Column("orderStatusId")] 
    public string OrderStatusId { get; set; }= default!;
    
    public OrderStatus OrderStatus { get; set; } = default!;
    

}