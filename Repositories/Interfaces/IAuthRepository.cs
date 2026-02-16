using KurdStudio.AdminApi.Models.Entities;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<AdminUser?> GetUserByUsernameAsync(string username);
    Task<int> CreateUserAsync(string username, string passwordHash, string? displayName, string? email);
    Task UpdateLastLoginAsync(int id);
    Task SaveRefreshTokenAsync(int adminUserId, string token, DateTime expiresAt);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
