using Shared.Models.Models;
using Shared.Models.response;

namespace BL.Interfaces
{
    public interface IBookmarkService
    {
        Response<GitHubRepository> AddBookmarkAsync(string userId, GitHubRepository repo);
        Response<List<GitHubRepository>> GetBookmarksAsync(string userId);
    }
}
