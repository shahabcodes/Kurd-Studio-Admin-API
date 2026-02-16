using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Entities;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class DashboardRepository : IDashboardRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DashboardRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<DashboardStats> GetStatsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<DashboardStats>(
            "usp_Admin_GetDashboardStats",
            commandType: CommandType.StoredProcedure
        );
    }
}
