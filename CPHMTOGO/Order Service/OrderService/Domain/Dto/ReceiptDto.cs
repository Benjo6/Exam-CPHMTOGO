using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class ReceiptDto : BaseEntityDto
{
    public float Amount { get; set; }
    public DateTime Time { get; set; }
    public Order Order { get; set; }

}