using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Models.Models
{
    public class GitHubSearchResult
    {
        [JsonPropertyName("total_count")]
        [Range(0, int.MaxValue, ErrorMessage = "TotalCount must be a non-negative number")]
        public int TotalCount { get; set; }

        [JsonPropertyName("items")]
        [Required(ErrorMessage = "Items are required")]
        [MinLength(1, ErrorMessage = "At least one repository is required")]
        public List<GitHubRepository> Items { get; set; } = new();
    }

    public class GitHubRepository
    {
        [JsonPropertyName("id")]
        [Range(1, int.MaxValue, ErrorMessage = "Repository ID must be greater than 0")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Repository name is required")]
        [MinLength(1, ErrorMessage = "Repository name cannot be empty")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("owner")]
        [Required(ErrorMessage = "Owner information is required")]
        public GitHubOwner Owner { get; set; } = new();
    }

    public class GitHubOwner
    {
        [JsonPropertyName("avatar_url")]
        [Required(ErrorMessage = "Avatar URL is required")]
        [Url(ErrorMessage = "Avatar URL must be a valid URL")]
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
