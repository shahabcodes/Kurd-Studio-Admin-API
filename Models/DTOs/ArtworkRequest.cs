namespace KurdStudio.AdminApi.Models.DTOs;

public record ArtworkRequest(
    string Title,
    string Slug,
    int ArtworkTypeId,
    int ImageId,
    string? Description,
    bool IsFeatured,
    int DisplayOrder
);
