using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Endpoints;

public static class DashboardEndpoints
{
    public static RouteGroupBuilder MapDashboardEndpoints(this RouteGroupBuilder group)
    {
        var dashboard = group.MapGroup("/dashboard").WithTags("Dashboard");

        dashboard.MapGet("/stats", GetStats)
            .WithName("GetDashboardStats")
            .RequireAuthorization();

        return group;
    }

    private static async Task<IResult> GetStats(IDashboardRepository repository)
    {
        var stats = await repository.GetStatsAsync();
        return Results.Ok(stats);
    }
}
