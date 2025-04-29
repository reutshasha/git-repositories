using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RepositoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public SearchController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        /// <summary>
        /// Searches for GitHub repositories based on a query.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page number for pagination (default is 1).</param>
        /// <param name="perPage">The number of results per page (default is 20).</param>
        /// <returns>A list of repositories that match the search query.</returns>
        [HttpGet]
        public async Task<IActionResult> SearchRepositories([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int perPage = 20)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { error = "The 'query' parameter is required." });

            var result = await _gitHubService.SearchAsync(query, page, perPage);
            return StatusCode((int)result.StatusCode, new
            {
                message = result.Message,
                data = result.Data
            });
        }
    }
}
