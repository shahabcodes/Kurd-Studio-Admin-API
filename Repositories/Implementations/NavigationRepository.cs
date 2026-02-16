using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class NavigationRepository : INavigationRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public NavigationRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<NavigationItem>(
            "usp_Admin_GetNavigationItems",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CreateNavigationItemAsync(NavigationItem item)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<int>(
            "usp_Admin_CreateNavigationItem",
            new { item.Label, item.Link, item.IconSvg, item.DisplayOrder, item.IsActive },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateNavigationItemAsync(NavigationItem item)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateNavigationItem",
            new { item.Id, item.Label, item.Link, item.IconSvg, item.DisplayOrder, item.IsActive },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteNavigationItemAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_DeleteNavigationItem",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<SocialLink>> GetSocialLinksAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<SocialLink>(
            "usp_Admin_GetSocialLinks",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CreateSocialLinkAsync(SocialLink link)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<int>(
            "usp_Admin_CreateSocialLink",
            new { link.Platform, link.Url, link.IconSvg, link.DisplayOrder, link.IsActive },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateSocialLinkAsync(SocialLink link)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateSocialLink",
            new { link.Id, link.Platform, link.Url, link.IconSvg, link.DisplayOrder, link.IsActive },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteSocialLinkAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_DeleteSocialLink",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }
}
