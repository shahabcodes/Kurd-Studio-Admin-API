namespace KurdStudio.AdminApi.Models.DTOs;

public record NavigationItemRequest(
    string Label,
    string Link,
    string? IconSvg,
    int DisplayOrder,
    bool IsActive
);
