using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace ServiceFinder.BLL.Services
{
    public class AuthServiceClient : IAuthServiceClient
    {
        private readonly HttpClient _httpClient;

        public AuthServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var registerRequest = new
            {
                email,
                password
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7292/api/Auth/register", registerRequest);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // This will allow matching on camelCase properties
            };

            // Deserialize the response into a strongly typed object with the correct options
            var authResponse = JsonSerializer.Deserialize<RegisterResponse>(responseBody, options);

            if (authResponse == null || string.IsNullOrEmpty(authResponse.Auth0Id))
            {
                throw new Exception("Auth0Id not found in response");
            }

            return authResponse.Auth0Id;
        }

        // Define a class that matches the structure of the JSON response
        public class RegisterResponse
        {
            public string Auth0Id { get; set; }
            public string Error { get; set; }
        }



        public async Task<string> LoginUserAsync(string email, string password)
        {
            var loginRequest = new
            {
                email,
                password
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7292/api/auth/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<dynamic>(responseBody);

            return (string)authResponse.Token;
        }

        public async Task<User> GetUserByAuth0IdAsync(string auth0Id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7292/api/Auth/user/{auth0Id}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(responseBody);
        }

        public async Task AssignRoleToUserAsync(string userId, string roleName)
        {
            var assignRoleRequest = new
            {
                UserId = userId,
                RoleName = roleName
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7292/api/auth/assign-role", assignRoleRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var removeRoleRequest = new
            {
                UserId = userId,
                RoleName = roleName
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7292/api/auth/remove-role", removeRoleRequest);
            response.EnsureSuccessStatusCode();
        }
    }
}
