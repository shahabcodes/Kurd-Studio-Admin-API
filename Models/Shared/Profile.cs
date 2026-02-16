namespace KurdStudio.AdminApi.Models.Shared;

public class Profile
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Tagline { get; set; }
    public string? Bio { get; set; }
    public int? AvatarImageId { get; set; }
    public string? Email { get; set; }
    public string? InstagramUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? ArtworksCount { get; set; }
    public string? PoemsCount { get; set; }
    public string? YearsExperience { get; set; }
    public DateTime UpdatedAt { get; set; }
}
