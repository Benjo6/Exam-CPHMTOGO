using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using OrderService.Domain.EntityHelper;

namespace OrderService.Domain;
[Table("OrderStatus"),MessagePackObject(keyAsPropertyName:true)]
public class OrderStatus : EntityWithId<Guid>
{
    [Column("id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } 

    [Column("timeStamp")]
    public DateTime TimeStamp { get; set; }
    
    [Column("status")] 
    public Status Status { get; set; }
    
}