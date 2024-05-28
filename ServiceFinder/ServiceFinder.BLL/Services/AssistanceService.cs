using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceService : GenericService<AssistanceEntity, Assistance>, IAssistanceService
    {
        public AssistanceService(IAssistanceRepository serviceRepository, IMapper mapper) : base(serviceRepository, mapper)
        {
        }
    }
}
