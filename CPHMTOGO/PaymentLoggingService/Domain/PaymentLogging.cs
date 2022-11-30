using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace PaymentLoggingService.Domain;

[Table("Payment"),MessagePackObject(keyAsPropertyName:true)]
public class PaymentLogging : BaseEntity
{
    [Column("from")]
    public Guid From { get; set; }
    [Column("to")]
    public Guid To { get; set; }
    [Column("amount")]
    public double Amount { get; set; }
    [Column("type")]
    public string Type { get; set; }
}