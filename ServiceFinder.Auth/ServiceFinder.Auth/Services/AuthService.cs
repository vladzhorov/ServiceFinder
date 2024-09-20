using Newtonsoft.Json;
using ServiceFinder.Auth.Interfaces;
using ServiceFinder.Auth.Models;
using System.Net.Http.Headers;
using System.Text;

namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _auth0Domain;
        private readonly string _auth0ClientId;
        private readonly string _auth0ClientSecret;
        private readonly string _auth0Audience;
        private readonly string _auth0ManagementApiToken;
        private readonly IUserRepository _userRepository;

        public AuthService(HttpClient httpClient, IConfiguration configuration, IUserRepository userRepository)
        {
            _httpClient = httpClient;
            _auth0Domain = configuration["Auth0:Domain"];
            _auth0ClientId = configuration["Auth0:ClientId"];
            _auth0ClientSecret = configuration["Auth0:ClientSecret"];
            _auth0Audience = configuration["Auth0:Audience"];
            _userRepository = userRepository;
        }
        private async Task<string> GetManagementApiTokenAsync()
        {
            var tokenRequestData = new
            {
                grant_type = "client_credentials",
                client_id = _auth0ClientId,
                client_secret = _auth0ClientSecret,
                audience = $"https://{_auth0Domain}/api/v2/",
                scope = "create:users"
            };

            var response = await _httpClient.PostAsJsonAsync($"https://{_auth0Domain}/oauth/token", tokenRequestData);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Auth0 API call failed: {errorResponse}");
            }

            var tokenResponse = await response.Content.ReadAsStringAsync();
            var tokenData = JsonConvert.DeserializeObject<dynamic>(tokenResponse);

            if (tokenData?.access_token == null)
            {
                throw new Exception("Access token not found in the response.");
            }

            return tokenData.access_token.ToString();
        }


        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var userData = new
            {
                email,
                password = passwordHash,
                connection = "Username-Password-Authentication"
            };

            var token = await GetManagementApiTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"https://{_auth0Domain}/api/v2/users";

            var response = await _httpClient.PostAsJsonAsync(url, userData);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Auth0 API call failed: {errorResponse}");
            }

            var userResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<dynamic>(userResponse);
            var auth0Id = user.user_id.ToString();

            var authUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Auth0Id = auth0Id,
                Email = email,
                PasswordHash = passwordHash
            };

            await _userRepository.AddAsync(authUser);

            return auth0Id;
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            // Поиск пользователя по email
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                Console.WriteLine($"User not found for email: {email}");
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            // Проверка пароля
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                Console.WriteLine($"Password verification failed for email: {email}");
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            // Подготовка данных для запроса токена
            var tokenData = new
            {
                grant_type = "password",
                username = email,
                password = user.PasswordHash,
                audience = $"https://{_auth0Domain}/api/v2/",
                client_id = _auth0ClientId,
                client_secret = _auth0ClientSecret,
            };

            // Выполнение запроса
            var response = await _httpClient.PostAsJsonAsync($"https://{_auth0Domain}/oauth/token", tokenData);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Auth0 API call failed: {errorResponse}");
            }

            // Обработка ответа
            var tokenResponse = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<dynamic>(tokenResponse);

            if (token?.access_token == null)
            {
                throw new Exception("Access token not found in the response.");
            }

            return token.access_token.ToString();
        }


        public async Task LogoutUserAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Domain}/v2/logout")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { returnTo = "http://localhost:3000" }), Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Auth0 API call failed: {errorResponse}");
            }
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return;

            var role = await _userRepository.GetRoleByNameAsync(roleName);
            if (role == null) return;

            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return;

            var role = await _userRepository.GetRoleByNameAsync(roleName);
            if (role == null) return;

            if (user.Roles.Contains(role))
            {
                user.Roles.Remove(role);
                await _userRepository.UpdateAsync(user);
            }
        }
    }
}
