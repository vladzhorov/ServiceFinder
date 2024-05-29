using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route("api/assistances")]
    public class AssistanceController : ControllerBase
    {
        private readonly IAssistanceService _AssistanceService;
        private readonly IMapper _mapper;

        public AssistanceController(IMapper mapper, IAssistanceService AssistanceService)
        {
            _mapper = mapper;
            _AssistanceService = AssistanceService;
        }

        [HttpGet]
        public async Task<List<AssistanceViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var Assistances = await _AssistanceService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<AssistanceViewModel>>(Assistances);
        }

        [HttpGet("{id}")]
        public async Task<AssistanceViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var Assistance = await _AssistanceService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(Assistance);
        }

        [HttpPost]
        public async Task<AssistanceViewModel> Create(CreateAssistanceViewModel viewModel, CancellationToken cancellationToken)
        {
            var assistance = _mapper.Map<Assistance>(viewModel);
            var result = await _AssistanceService.CreateAsync(assistance, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<AssistanceViewModel> Update(Guid id, UpdateAssistanceViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<Assistance>(viewModel);
            modelToUpdate.Id = id;
            var result = await _AssistanceService.UpdateAsync(modelToUpdate, cancellationToken);
            return _mapper.Map<AssistanceViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _AssistanceService.DeleteAsync(id, cancellationToken);
        }
    }
}