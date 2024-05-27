using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceService : GenericService<ServiceEntity, Service>, IAssistanceService
    {
        public AssistanceService(IServiceRepository serviceRepository, IMapper mapper) : base(serviceRepository, mapper)
        {
        }
    }
}
