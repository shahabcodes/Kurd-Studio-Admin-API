using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class ContactRepository : IContactRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ContactRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ContactSubmission>> GetAllAsync(bool onlyUnread = false)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<ContactSubmission>(
            "usp_Admin_GetContactSubmissions",
            new { OnlyUnread = onlyUnread },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task MarkAsReadAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_MarkContactRead",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_DeleteContact",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }
}
