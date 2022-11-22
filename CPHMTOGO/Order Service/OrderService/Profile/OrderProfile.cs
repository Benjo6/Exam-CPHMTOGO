using OrderService.Domain;
using OrderService.Domain.Dto;

namespace OrderService.Profile;

public class OrderProfile:AutoMapper.Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(y => y.OrderStatus, x => x.MapFrom(y => y.OrderStatus));
        CreateMap<OrderDto, Order>();


        
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(y => y.Order, x => x.MapFrom(y => y.Order));
        CreateMap<OrderItemDto, OrderItem>();
        
        CreateMap<OrderStatus, OrderStatusDto>();
        CreateMap<OrderStatusDto, OrderStatus>();

        CreateMap<Receipt, ReceiptDto>()
            .ForMember(y => y.Order, x => x.MapFrom(y => y.Order));
        CreateMap<ReceiptDto, Receipt>();
    }
}