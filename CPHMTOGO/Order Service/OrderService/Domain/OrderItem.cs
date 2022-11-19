using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using OrderService.Domain.EntityHelper;

namespace OrderService.Domain;

[Table("OrderItem"),MessagePackObject(keyAsPropertyName:true)]
public class OrderItem : EntityWithId<Guid>
{
    [Column("id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } 
    
    [Column("preference")] 
    public string Preference { get; set; } = default!;
    
    [Column("price")] 
    public float Price { get; set; }
    
    [Column("quantity")] 
    public int Quantity { get; set; }

    [Column("orderId")] public string OrderId { get; set; } = default!;

    public Order Order { get; set; } = default!;

}