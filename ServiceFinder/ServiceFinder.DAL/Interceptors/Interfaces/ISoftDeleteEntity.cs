namespace ServiceFinder.DAL.Interceptors.Interfaces
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
