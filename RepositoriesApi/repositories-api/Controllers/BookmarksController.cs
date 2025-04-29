using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoriesApi.Filters;
using Shared.Models.Models;
using Shared.Models.response;

namespace RepositoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkSessionService;

        public BookmarksController(IBookmarkService bookmarkSessionService)
        {
            _bookmarkSessionService = bookmarkSessionService;
        }

        /// <summary>
        /// Adds a GitHub repository to the user's bookmarks.
        /// </summary>
        /// <param name="repo">The GitHub repository to be bookmarked.</param>
        /// <returns>A response indicating whether the bookmark was successfully added.</returns>
        [RequireUserIdFilter]
        [HttpPost]
        public IActionResult Post([FromBody] GitHubRepository repo)
        {
            var userId = HttpContext.Items["UserId"] as string;

            var response = _bookmarkSessionService.AddBookmarkAsync(userId, repo);

            return StatusCode((int)response.StatusCode, new
            {
                message = response.Message,
                data = response.Data
            });
        }

        /// <summary>
        /// Retrieves all the bookmarks for the currently authenticated user.
        /// </summary>
        /// <returns>A list of the user's bookmarked GitHub repositories.</returns>
        [RequireUserIdFilter]
        [HttpGet]
        public IActionResult Get()
        {
            var userId = HttpContext.Items["UserId"] as string;

            var bookmarks = _bookmarkSessionService.GetBookmarksAsync(userId);
            return StatusCode((int)bookmarks.StatusCode, new
            {
                message = bookmarks.Message,
                data = bookmarks.Data
            });
        }
    }
}
