using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.API.Constants;
using ServiceFinder.OrderService.API.ViewModels.Order;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.API.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Orders)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderViewModel> _createOrderViewModelValidator;
        public OrderController(IValidator<CreateOrderViewModel> createOrderViewModelValidator,
            IMapper mapper,
            IOrderAppService orderAppService)
        {
            _createOrderViewModelValidator = createOrderViewModelValidator;
            _mapper = mapper;
            _orderAppService = orderAppService;
        }

        [HttpGet("{id}")]
        public async Task<OrderViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id, cancellationToken);
            return _mapper.Map<OrderViewModel>(order);
        }

        [HttpGet]
        public async Task<PagedResult<OrderViewModel>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = await _orderAppService.GetAllOrderAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<PagedResult<OrderViewModel>>(pagedResult);
        }
        [HttpPost]
        public async Task<OrderViewModel> Create(CreateOrderViewModel viewModel, CancellationToken cancellationToken)
        {
            await _createOrderViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
            var orderDto = _mapper.Map<OrderDto>(viewModel);
            var result = await _orderAppService.CreateOrderAsync(orderDto, viewModel.BaseRatePerMinute, viewModel.BaseRateDurationInMinutes, cancellationToken);
            return _mapper.Map<OrderViewModel>(result);
        }

        [HttpPut(ApiRoutes.Status)]
        public async Task<OrderViewModel> UpdateStatus(Guid id, OrderStatus newStatus, string email, CancellationToken cancellationToken)
        {
            await _orderAppService.UpdateOrderStatusAsync(id, newStatus, email, cancellationToken);
            var updatedOrder = await _orderAppService.GetOrderByIdAsync(id, cancellationToken);
            return _mapper.Map<OrderViewModel>(updatedOrder);
        }
    }
}
