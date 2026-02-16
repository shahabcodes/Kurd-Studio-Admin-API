namespace KurdStudio.AdminApi.Models.DTOs;

public record SocialLinkRequest(
    string Platform,
    string Url,
    string? IconSvg,
    int DisplayOrder,
    bool IsActive
);
