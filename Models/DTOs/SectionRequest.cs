namespace KurdStudio.AdminApi.Models.DTOs;

public record SectionRequest(
    string? Tag,
    string? Title,
    string? Subtitle,
    int DisplayOrder,
    bool IsActive
);
