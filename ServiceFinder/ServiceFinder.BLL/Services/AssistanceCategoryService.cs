using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceCategoryService : GenericService<AssistanceCategoryEntity, AssistanceCategory>, IAssistanceCategoryService
    {
        public AssistanceCategoryService(IAssistanceCategoryRepository serviceCategoryRepository, IMapper mapper) : base(serviceCategoryRepository, mapper)
        {
        }
    }
}
