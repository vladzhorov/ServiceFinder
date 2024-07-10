using AutoMapper;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<OrderRequest, OrderRequestDto>();
            CreateMap<OrderRequestDto, OrderRequest>();
        }
    }
}
