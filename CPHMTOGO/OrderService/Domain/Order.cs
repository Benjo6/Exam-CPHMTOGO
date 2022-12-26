using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace OrderService.Domain;
[Table("Order"),MessagePackObject(keyAsPropertyName:true)]

public class Order : BaseEntity
{

    [Column("addressId")] 
    public Guid AddressId { get; set; } 

    [Column("customerId")] public Guid CustomerId { get; set; } 

    [Column("employeeId")] public Guid EmployeeId { get; set; } 
    
    [Column("restaurantId")] public Guid RestaurantId { get; set; }
    
    [Column("ordreStatusId")] 
    public Guid OrderStatusId { get; set; }
    
    public OrderStatus OrderStatus { get; set; } 


    

}