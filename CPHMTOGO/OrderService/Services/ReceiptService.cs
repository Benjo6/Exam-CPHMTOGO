using AutoMapper;
using Core.Repository;
using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class ReceiptService : BaseService<Receipt,ReceiptDto>, IReceiptService
{
    public ReceiptService(IReceiptRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public async Task<ReceiptDto> GetByOrderId(Guid orderid)
    {
        return _mapper.Map<ReceiptDto>(_repository.GetByCondition(x => x.OrderId==orderid).SingleOrDefault());
         
    }
}