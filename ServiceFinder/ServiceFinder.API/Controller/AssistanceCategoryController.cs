using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.AssistanceCategory;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.assistancesCategory)]
    public class AssistanceCategoryController : ControllerBase
    {
        private readonly IAssistanceCategoryService _AssistanceCategoryService;
        private readonly IMapper _mapper;

        public AssistanceCategoryController(IMapper mapper, IAssistanceCategoryService AssistanceService)
        {
            _mapper = mapper;
            _AssistanceCategoryService = AssistanceService;
        }

        [HttpGet]
        public async Task<List<AssistanceCategoryViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var assistances = await _AssistanceCategoryService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<AssistanceCategoryViewModel>>(assistances);
        }

        [HttpGet("{id}")]
        public async Task<AssistanceCategoryViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var assistance = await _AssistanceCategoryService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AssistanceCategoryViewModel>(assistance);
        }

        [HttpPost]
        public async Task<AssistanceCategoryViewModel> Create(CreateAssistanceCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var assistance = _mapper.Map<AssistanceCategory>(viewModel);
            var result = await _AssistanceCategoryService.CreateAsync(assistance, cancellationToken);
            return _mapper.Map<AssistanceCategoryViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<AssistanceCategoryViewModel> Update(Guid id, UpdateAssistanceCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<AssistanceCategory>(viewModel);
            var result = await _AssistanceCategoryService.UpdateAsync(id, modelToUpdate, cancellationToken);
            return _mapper.Map<AssistanceCategoryViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _AssistanceCategoryService.DeleteAsync(id, cancellationToken);
        }
    }
}