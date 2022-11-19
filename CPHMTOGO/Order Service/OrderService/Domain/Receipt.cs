using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using OrderService.Domain.EntityHelper;

namespace OrderService.Domain;
[Table("Receipt"),MessagePackObject(keyAsPropertyName:true)]
public class Receipt : EntityWithId<Guid>
{
    [Column("id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } 
    
    
    [Column("amount")] 
    public float Amount { get; set; }
    
    [Column("time")] 
    public DateTime Time { get; set; }
    
    [Column("orderId")] 
    public string OrderId { get; set; }= default!;
    
    public Order Order { get; set; } = default!;
    
}