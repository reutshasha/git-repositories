using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Models.Models;
using Shared.Models.response;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BL.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKeyPrefix = "Bookmarks_";

        public BookmarkService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Response<GitHubRepository> AddBookmarkAsync(string userId, GitHubRepository repo)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new Response<GitHubRepository>(HttpStatusCode.BadRequest, "User ID cannot be empty.");
            }

            if (repo == null || repo.Id == 0)
            {
                return new Response<GitHubRepository>(HttpStatusCode.BadRequest, "Invalid repository data.");
            }

            var key = $"{SessionKeyPrefix}{userId}";
            var session = _httpContextAccessor.HttpContext?.Session;

            var json = session?.GetString(key);
            var bookmarks = string.IsNullOrEmpty(json)
                ? new List<GitHubRepository>()
                : JsonSerializer.Deserialize<List<GitHubRepository>>(json) ?? new List<GitHubRepository>();

            if (bookmarks.All(b => b.Id != repo.Id))
            {
                bookmarks.Add(repo);
                session?.SetString(key, JsonSerializer.Serialize(bookmarks));
                return new Response<GitHubRepository>(HttpStatusCode.OK, "Repository bookmarked successfully", repo);
            }
            else
            {
                return new Response<GitHubRepository>(HttpStatusCode.Conflict, "Repository is already bookmarked.");
            }
        }

        public  Response<List<GitHubRepository>> GetBookmarksAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new Response<List<GitHubRepository>>(HttpStatusCode.BadRequest, "User ID cannot be empty.");
            }

            var key = $"{SessionKeyPrefix}{userId}";

            var json = _httpContextAccessor.HttpContext?.Session?.GetString(key);

            var bookmarks = string.IsNullOrEmpty(json)
                ? new List<GitHubRepository>()
                : JsonSerializer.Deserialize<List<GitHubRepository>>(json) ?? new List<GitHubRepository>();

            return new Response<List<GitHubRepository>>(HttpStatusCode.OK, "Bookmarks retrieved successfully.", bookmarks);
        }

    }
}
