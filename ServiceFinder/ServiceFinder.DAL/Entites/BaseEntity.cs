using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}