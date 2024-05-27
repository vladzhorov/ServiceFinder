using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceCategoryService : GenericService<ServiceCategoryEntity, ServiceCategory>, IAssistanceCategoryService
    {
        public AssistanceCategoryService(IServiceCategoryRepository serviceCategoryRepository, IMapper mapper) : base(serviceCategoryRepository, mapper)
        {
        }
    }
}
