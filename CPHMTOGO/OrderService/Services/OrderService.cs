using AutoMapper;
using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class OrderService : BaseService<Order,OrderDto>, IOrderService
{
    private readonly IOrderStatusService _statusService;
    private readonly IReceiptRepository _receiptRepository;
    private readonly IOrderItemRepository _itemRepository;
    public OrderService(IOrderRepository repository, IMapper mapper, IOrderStatusService statusService, IReceiptRepository receiptRepository, IOrderItemRepository itemRepository) : base(repository, mapper)
    {
        _statusService = statusService;
        _receiptRepository = receiptRepository;
        _itemRepository = itemRepository;
    }
    

    public async Task<OrderDto> CreateOrderTask(OrderDto entity, List<CreateOrderItemDto> itemDtos)
    {
        var orderstatus = new OrderStatusDto();
        var resultStatus= _statusService.Create(orderstatus);
        entity.OrderStatusId = resultStatus.Result.Id;
        var result = await base.Create(entity);
        foreach (var item in _mapper.Map<List<OrderItem>>(itemDtos))
        {
            item.OrderId = result.Id;
            _itemRepository.Create(item);
        }

        var receipt = new ReceiptDto();
        foreach (var item in itemDtos.Select(i=>new{i.Price,i.Quantity}))
        {
            receipt.Amount += item.Price*item.Quantity;
        }

        receipt.Time = DateTime.UtcNow;
        receipt.OrderId = result.Id;
        _receiptRepository.Create(_mapper.Map<Receipt>(receipt));
        return result;    
    }

    public async Task<IEnumerable<OrderDto>> GetOpenOrders()
    {
       var orders= _repository.GetByCondition(t => t.OrderStatus.Status == Status.STARTED.ToString());
       return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<int> NumberOfOpenOrders()
    {
        var orders =  _repository.GetByCondition(t => t.OrderStatus.Status == Status.STARTED.ToString());
        return orders.Count();
    }
}