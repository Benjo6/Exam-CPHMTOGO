using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace OrderService.Domain;

[Table("OrderItem"),MessagePackObject(keyAsPropertyName:true)]
public class OrderItem : BaseEntity
{

    
    [Column("preference")] 
    public string Preference { get; set; } = default!;
    
    [Column("price")] 
    public double Price { get; set; }
    
    [Column("quantity")] 
    public int Quantity { get; set; }

    [Column("orderId")] public Guid OrderId { get; set; } = default!;

    public Order Order { get; set; } = default!;

}