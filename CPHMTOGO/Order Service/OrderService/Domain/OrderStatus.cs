using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace OrderService.Domain;
[Table("OrdreStatus"),MessagePackObject(keyAsPropertyName:true)]
public class OrderStatus : BaseEntity
{


    [Column("timeStamp")]
    public DateTime TimeStamp { get; set; }
    
    [Column("status")] 
    public string Status { get; set; }
    
    
}