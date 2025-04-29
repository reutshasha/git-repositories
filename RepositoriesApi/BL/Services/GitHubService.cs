using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Models.Models;
using Shared.Models.response;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly ILogService _logService;
        private readonly HttpClient _http;
        private readonly string _baseAddress;


        public GitHubService(ILogService logService, HttpClient http, IConfiguration configuration)
        {
            _logService = logService;
            _http = http;

            var gitHubApiConfig = configuration.GetSection("GitHubApi");

            var baseAddress = gitHubApiConfig["BaseAddress"];
            var userAgent = gitHubApiConfig["UserAgent"];

            _http.BaseAddress = new Uri(baseAddress);
            _http.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

        }
        public async Task<Response<List<GitHubSearchResult>>> SearchAsync(string query, int page = 1, int perPage = 20)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                _logService.LogWarning("Search query cannot be empty.");
                return new Response<List<GitHubSearchResult>>(
                    HttpStatusCode.BadRequest,
                    "Search query cannot be empty."
                );
            }

            try
            {
                var url = $"search/repositories?q={Uri.EscapeDataString(query)}&page={page}&per_page={perPage}";
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logService.LogError(new Exception($"GitHub API error: {response.StatusCode}"), errorContent);
                    return new Response<List<GitHubSearchResult>>(
                        HttpStatusCode.InternalServerError, 
                        "Error retrieving data from GitHub."
                        );
                }

                var content = await response.Content.ReadAsStringAsync();
                var searchResult = JsonSerializer.Deserialize<GitHubSearchResult>(content);

                if (searchResult == null || searchResult.Items == null || !searchResult.Items.Any())
                {
                    return new Response<List<GitHubSearchResult>>(HttpStatusCode.NotFound, "No repositories found.");
                }

                return new Response<List<GitHubSearchResult>>(HttpStatusCode.OK, "Repositories retrieved successfully.", new List<GitHubSearchResult> { searchResult });
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "SearchAsync");
                return new Response<List<GitHubSearchResult>>(HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }
    }

}
