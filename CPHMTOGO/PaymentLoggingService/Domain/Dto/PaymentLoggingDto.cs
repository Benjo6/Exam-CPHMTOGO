using System.ComponentModel.DataAnnotations;
using Core.Entity.Dtos;

namespace PaymentLoggingService.Domain.Dto;

public class PaymentLoggingDto : BaseEntityDto
{
    [Required(ErrorMessage = "Who is sending this????")]
    public Guid From { get; set; }
    [Required(ErrorMessage = "Who is getting this money?????")]
    public Guid To { get; set; }
    [Required(ErrorMessage = "How much money is involved in this transaction is currently null")]
    public double Amount { get; set; }
    [Required(ErrorMessage="What type of transaction is this???")]
    public string Type { get; set; }
}