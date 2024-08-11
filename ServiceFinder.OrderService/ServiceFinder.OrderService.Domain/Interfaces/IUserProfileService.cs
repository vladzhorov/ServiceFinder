using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfile?> GetUserProfileAsync(Guid userId);
        Task<Assistance?> GetAssistanceAsync(Guid assistanceId);
    }
}
