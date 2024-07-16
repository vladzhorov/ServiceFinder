using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.ViewModels.OrderRequest;
using ServiceFinder.Domain.PaginationObjects;
using ServiceFinder.OrderService.API.Constants;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.API.Controllers
{
    [ApiController]
    [Route(ApiRoutes.OrderRequests)]
    public class OrderRequestController : ControllerBase
    {
        private readonly IOrderRequestAppService _orderRequestAppService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderRequestViewModel> _createOrderRequestViewModelValidator;

        public OrderRequestController(IValidator<CreateOrderRequestViewModel> createOrderRequestViewModelValidator,
            IMapper mapper,
            IOrderRequestAppService orderRequestAppService)
        {
            _createOrderRequestViewModelValidator = createOrderRequestViewModelValidator;
            _mapper = mapper;
            _orderRequestAppService = orderRequestAppService;
        }

        [HttpGet("{id}")]
        public async Task<OrderRequestViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var orderRequest = await _orderRequestAppService.GetOrderRequestByIdAsync(id, cancellationToken);
            return _mapper.Map<OrderRequestViewModel>(orderRequest);
        }

        [HttpPost]
        public async Task<OrderRequestViewModel> Create(CreateOrderRequestViewModel viewModel, CancellationToken cancellationToken)
        {
            await _createOrderRequestViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
            var orderRequestDto = _mapper.Map<OrderRequestDto>(viewModel);
            var result = await _orderRequestAppService.CreateOrderRequestAsync(orderRequestDto, cancellationToken);
            return _mapper.Map<OrderRequestViewModel>(result);
        }

        [HttpGet]
        public async Task<PagedResult<OrderRequestViewModel>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = await _orderRequestAppService.GetAllOrderRequestAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<PagedResult<OrderRequestViewModel>>(pagedResult);
        }

        [HttpPut(ApiRoutes.Status)]
        public async Task<OrderRequestViewModel> UpdateStatus(Guid id, OrderRequestStatus newStatus, CancellationToken cancellationToken)
        {
            await _orderRequestAppService.UpdateOrderRequestStatusAsync(id, newStatus, cancellationToken);
            var updatedOrder = await _orderRequestAppService.GetOrderRequestByIdAsync(id, cancellationToken);
            return _mapper.Map<OrderRequestViewModel>(updatedOrder);
        }
    }
}
