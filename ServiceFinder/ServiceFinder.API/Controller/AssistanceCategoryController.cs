﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.Constants;
using ServiceFinder.API.Exceptions;
using ServiceFinder.API.ViewModels.AssistanceCategory;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.AssistancesCategory)]
    public class AssistanceCategoryController : ControllerBase
    {
        private readonly IAssistanceCategoryService _assistanceCategoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAssistanceCategoryViewModel> _createAssistanceCategoryViewModelValidator;
        private readonly IValidator<UpdateAssistanceCategoryViewModel> _updateAssistanceCategoryViewModelValidator;

        public AssistanceCategoryController(IValidator<UpdateAssistanceCategoryViewModel> updateAssistanceCategoryViewModelValidator,
            IValidator<CreateAssistanceCategoryViewModel> createAssistanceCategoryViewModelValidator,
            IMapper mapper,
            IAssistanceCategoryService assistanceService)

        {
            _createAssistanceCategoryViewModelValidator = createAssistanceCategoryViewModelValidator;
            _updateAssistanceCategoryViewModelValidator = updateAssistanceCategoryViewModelValidator;
            _mapper = mapper;
            _assistanceCategoryService = assistanceService;
        }

        [HttpGet]
        public async Task<List<AssistanceCategoryViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var assistances = await _assistanceCategoryService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<AssistanceCategoryViewModel>>(assistances);
        }

        [HttpGet("{id}")]
        public async Task<AssistanceCategoryViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var assistance = await _assistanceCategoryService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AssistanceCategoryViewModel>(assistance);
        }

        [HttpPost]
        public async Task<AssistanceCategoryViewModel> Create(CreateAssistanceCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var validationResult = await _createAssistanceCategoryViewModelValidator.ValidateAsync(viewModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new FluentValidatorException(string.Join(", ", validationResult.Errors));
            }
            var assistance = _mapper.Map<AssistanceCategory>(viewModel);
            var result = await _assistanceCategoryService.CreateAsync(assistance, cancellationToken);
            return _mapper.Map<AssistanceCategoryViewModel>(result);

        }

        [HttpPut("{id}")]
        public async Task<AssistanceCategoryViewModel> Update(Guid id, UpdateAssistanceCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var validationResult = await _updateAssistanceCategoryViewModelValidator.ValidateAsync(viewModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new FluentValidatorException(string.Join(", ", validationResult.Errors));
            }
            var modelToUpdate = _mapper.Map<AssistanceCategory>(viewModel);
            var result = await _assistanceCategoryService.UpdateAsync(id, modelToUpdate, cancellationToken);
            return _mapper.Map<AssistanceCategoryViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _assistanceCategoryService.DeleteAsync(id, cancellationToken);
        }
    }
}