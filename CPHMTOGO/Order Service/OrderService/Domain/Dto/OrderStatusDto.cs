using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class OrderStatusDto : BaseEntityDto
{
    public DateTime TimeStamp { get; set; }
    public Status Status { get; set; }
    
}