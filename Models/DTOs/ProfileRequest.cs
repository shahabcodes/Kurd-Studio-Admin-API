namespace KurdStudio.AdminApi.Models.DTOs;

public record ProfileRequest(
    string Name,
    string? Tagline,
    string? Bio,
    int? AvatarImageId,
    string? Email,
    string? InstagramUrl,
    string? TwitterUrl,
    string? ArtworksCount,
    string? PoemsCount,
    string? YearsExperience
);
