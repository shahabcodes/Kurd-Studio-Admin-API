namespace KurdStudio.AdminApi.Models.DTOs;

public record ImageMetaDto(
    int Id,
    string FileName,
    string ContentType,
    string? AltText,
    int FileSize,
    int Width,
    int Height,
    string ImageUrl,
    string ThumbnailUrl,
    DateTime CreatedAt
);
