
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using System.Net.Http.Json;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly HttpClient _httpClient;

        public UserProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserProfile?> GetUserProfileAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/usersProfile/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserProfile>();
        }

        public async Task<Assistance?> GetAssistanceAsync(Guid assistanceId)
        {
            var response = await _httpClient.GetAsync($"api/assistances/{assistanceId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Assistance>();
        }
    }
}

