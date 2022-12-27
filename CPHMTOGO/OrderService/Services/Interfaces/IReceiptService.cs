using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;

namespace OrderService.Services.Interfaces;

public interface IReceiptService : IBaseService<Receipt,ReceiptDto>
{
    public Task<ReceiptDto> GetByOrderId(Guid orderid);

}