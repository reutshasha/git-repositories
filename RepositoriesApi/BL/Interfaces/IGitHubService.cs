using Shared.Models.Models;
using Shared.Models.response;

namespace BL.Interfaces
{
    public interface IGitHubService
    {
        Task<Response<List<GitHubSearchResult>>> SearchAsync(string query, int page = 1, int perPage = 20);
    }
}
