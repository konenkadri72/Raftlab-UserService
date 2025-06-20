using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Raftlab.ReqResPlugin.Models;
using System.Net;
using System.Text.Json;

namespace Raftlab.ReqResImplementation
{
    public class ReqResClientService : IReqResClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReqResClientService> _logger;
        public ReqResClientService(HttpClient httpClient, IConfiguration configuration, ILogger<ReqResClientService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection("ReqResBaseUrl").Value);
        }

        public async Task<UserListResponse> GetUsersAsync(int page)
        {
            try
            {
                var response = await _httpClient.GetAsync($"users?page={page}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch users for page {Page}. Status Code: {StatusCode}", page, response.StatusCode);
                    throw new HttpRequestException($"API error: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserListResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Deserialization error while fetching users for page {Page}", page);
                throw new ApplicationException("Deserialization failed", ex);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network or API error on GetUsersAsync");
                throw;
            }
        }

        public async Task<UserWrapper> GetUserByIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"users/{userId}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogWarning("User {UserId} not found", userId);
                    throw new Exception("User not found"); // or throw custom NotFoundException
                }

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<UserWrapper>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Deserialization error for user {UserId}", userId);
                throw new ApplicationException("Deserialization failed", ex);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network or API error on GetUserByIdAsync");
                throw;
            }
        }
    }
}
