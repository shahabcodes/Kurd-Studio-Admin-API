using KurdStudio.AdminApi.Models.Entities;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardStats> GetStatsAsync();
}
