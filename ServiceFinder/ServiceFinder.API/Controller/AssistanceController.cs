using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.Assistances)]
    public class AssistanceController : ControllerBase
    {
        private readonly IAssistanceService _assistanceService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAssistanceViewModel> _createAssistanceViewModelValidator;
        private readonly IValidator<UpdateAssistanceViewModel> _updateAssistanceViewModelValidator;
        public AssistanceController(IValidator<UpdateAssistanceViewModel> updateAssistanceViewModelValidator,
            IValidator<CreateAssistanceViewModel> createAssistanceViewModelValidator,
            IMapper mapper,
            IAssistanceService assistanceService)
        {
            _createAssistanceViewModelValidator = createAssistanceViewModelValidator;
            _updateAssistanceViewModelValidator = updateAssistanceViewModelValidator;
            _mapper = mapper;
            _assistanceService = assistanceService;
        }

        [HttpGet]
        public async Task<PagedResult<AssistanceViewModel>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = await _assistanceService.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<PagedResult<AssistanceViewModel>>(pagedResult);
        }
        [HttpGet("{id}")]
        public async Task<AssistanceViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var assistance = await _assistanceService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(assistance);
        }

        [HttpPost]
        public async Task<AssistanceViewModel> Create(CreateAssistanceViewModel viewModel, CancellationToken cancellationToken)
        {
            await _createAssistanceViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
            var assistance = _mapper.Map<Assistance>(viewModel);
            var result = await _assistanceService.CreateAsync(assistance, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<AssistanceViewModel> Update(Guid id, UpdateAssistanceViewModel viewModel, CancellationToken cancellationToken)
        {
            await _updateAssistanceViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
            var modelToUpdate = _mapper.Map<Assistance>(viewModel);
            var result = await _assistanceService.UpdateAsync(id, modelToUpdate, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _assistanceService.DeleteAsync(id, cancellationToken);
        }
    }
}