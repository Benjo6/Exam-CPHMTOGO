using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace OrderService.Domain;
[Table("Receipt"),MessagePackObject(keyAsPropertyName:true)]
public class Receipt : BaseEntity
{

    [Column("amount")] 
    public float Amount { get; set; }
    
    [Column("time")] 
    public DateTime Time { get; set; }
    
    [Column("orderId")] 
    public string OrderId { get; set; }= default!;
    
    public Order Order { get; set; } = default!;
    
}