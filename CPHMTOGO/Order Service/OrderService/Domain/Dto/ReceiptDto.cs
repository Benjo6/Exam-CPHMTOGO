using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class ReceiptDto : BaseEntityDto
{
    public double Amount { get; set; }
    public DateTime Time { get; set; }
    public Guid OrderId { get; set; }    
    

}