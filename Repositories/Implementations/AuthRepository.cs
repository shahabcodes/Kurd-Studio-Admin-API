using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Entities;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class AuthRepository : IAuthRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AuthRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<AdminUser?> GetUserByUsernameAsync(string username)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AdminUser>(
            "usp_Admin_GetUserByUsername",
            new { Username = username },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CreateUserAsync(string username, string passwordHash, string? displayName, string? email)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<int>(
            "usp_Admin_CreateUser",
            new { Username = username, PasswordHash = passwordHash, DisplayName = displayName, Email = email },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateLastLoginAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateLastLogin",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task SaveRefreshTokenAsync(int adminUserId, string token, DateTime expiresAt)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_SaveRefreshToken",
            new { AdminUserId = adminUserId, Token = token, ExpiresAt = expiresAt },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<RefreshToken>(
            "usp_Admin_GetRefreshToken",
            new { Token = token },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_RevokeRefreshToken",
            new { Token = token },
            commandType: CommandType.StoredProcedure
        );
    }
}
