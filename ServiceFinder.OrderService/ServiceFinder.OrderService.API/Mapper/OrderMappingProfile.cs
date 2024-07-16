using AutoMapper;
using ServiceFinder.API.ViewModels.OrderRequest;
using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.API.ViewModels.Order;
using ServiceFinder.OrderService.Application.DTOs;

namespace ServiceFinder.OrderService.API.Mapper
{
    public class OrderViewModelMappingProfile : Profile
    {
        public OrderViewModelMappingProfile()
        {
            CreateMap<OrderDto, OrderViewModel>().ReverseMap();
            CreateMap<CreateOrderViewModel, OrderDto>();

            CreateMap<OrderRequestDto, OrderRequestViewModel>().ReverseMap();
            CreateMap<CreateOrderRequestViewModel, OrderRequestDto>();

            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}
