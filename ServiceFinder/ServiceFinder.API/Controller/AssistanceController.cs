using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.assistances)]
    public class AssistanceController : ControllerBase
    {
        private readonly IAssistanceService _assistanceService;
        private readonly IMapper _mapper;

        public AssistanceController(IMapper mapper, IAssistanceService AssistanceService)
        {
            _mapper = mapper;
            _assistanceService = AssistanceService;
        }

        [HttpGet]
        public async Task<List<AssistanceViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var Assistances = await _assistanceService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<AssistanceViewModel>>(Assistances);
        }

        [HttpGet("{id}")]
        public async Task<AssistanceViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var Assistance = await _assistanceService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(Assistance);
        }

        [HttpPost]
        public async Task<AssistanceViewModel> Create(CreateAssistanceViewModel viewModel, CancellationToken cancellationToken)
        {
            var assistance = _mapper.Map<Assistance>(viewModel);
            var result = await _assistanceService.CreateAsync(assistance, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<AssistanceViewModel> Update(Guid id, UpdateAssistanceViewModel viewModel, CancellationToken cancellationToken)
        {
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