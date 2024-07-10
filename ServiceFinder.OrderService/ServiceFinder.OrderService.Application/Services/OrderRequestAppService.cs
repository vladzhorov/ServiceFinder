using AutoMapper;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Services;
namespace ServiceFinder.OrderService.Application
{
    public class OrderRequestAppService : IOrderRequestAppService
    {
        private readonly OrderRequestService _orderRequestService;
        private readonly IOrderRequestRepository _orderRequestRepository;
        private readonly IMapper _mapper;

        public OrderRequestAppService(OrderRequestService orderRequestService, IOrderRequestRepository orderRequestRepository, IMapper mapper)
        {
            _orderRequestService = orderRequestService;
            _orderRequestRepository = orderRequestRepository;
            _mapper = mapper;
        }

        public async Task<OrderRequestDto> CreateOrderRequestAsync(OrderRequestDto orderRequestDTO, CancellationToken cancellationToken)
        {
            var orderRequest = _mapper.Map<OrderRequest>(orderRequestDTO);
            await _orderRequestService.CreateOrderRequestAsync(orderRequest, cancellationToken);
            return _mapper.Map<OrderRequestDto>(orderRequest);
        }

        public async Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, CancellationToken cancellationToken)
        {
            await _orderRequestService.UpdateOrderRequestStatusAsync(orderRequestId, newStatus, cancellationToken);
        }

        public async Task<OrderRequestDto> GetOrderRequestByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var orderRequest = await _orderRequestRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<OrderRequestDto>(orderRequest);
        }
    }
}