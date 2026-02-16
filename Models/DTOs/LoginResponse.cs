namespace KurdStudio.AdminApi.Models.DTOs;

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    string Username,
    string DisplayName,
    DateTime ExpiresAt
);
