using OrderService.Domain;
using OrderService.Domain.Dto;

namespace OrderService.Profile;

public class OrderProfile:AutoMapper.Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(y => y.OrderStatusId, x => x.MapFrom(y => y.OrderStatusId));
        CreateMap<OrderDto, Order>();


        
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(y => y.OrderId, x => x.MapFrom(y => y.OrderId));
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<CreateOrderItemDto, OrderItem>();

        CreateMap<OrderStatus, OrderStatusDto>();
        CreateMap<OrderStatusDto, OrderStatus>();

        CreateMap<Receipt, ReceiptDto>()
            .ForMember(y => y.OrderId, x => x.MapFrom(y => y.OrderId));
        CreateMap<ReceiptDto, Receipt>();
    }
}