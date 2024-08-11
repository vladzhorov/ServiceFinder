using AutoMapper;
using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Domain.Exceptions;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.Application
{
    public class OrderRequestAppService : IOrderRequestAppService
    {
        private readonly IOrderRequestService _orderRequestService;
        private readonly IOrderRequestRepository _orderRequestRepository;
        private readonly IMapper _mapper;

        public OrderRequestAppService(IOrderRequestService orderRequestService, IOrderRequestRepository orderRequestRepository, IMapper mapper)
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

        public Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, string email, CancellationToken cancellationToken)
        {
            return _orderRequestService.UpdateOrderRequestStatusAsync(orderRequestId, newStatus, email, cancellationToken);
        }

        public async Task<OrderRequestDto> GetOrderRequestByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var orderRequest = await _orderRequestRepository.GetByIdAsync(id, cancellationToken) ?? throw new ModelNotFoundException(id);
            return _mapper.Map<OrderRequestDto>(orderRequest);
        }
        public async Task<PagedResult<OrderRequestDto>> GetAllOrderRequestAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedEntities = await _orderRequestRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            var mappedResult = _mapper.Map<PagedResult<OrderRequestDto>>(pagedEntities);
            return mappedResult;
        }
    }
}
