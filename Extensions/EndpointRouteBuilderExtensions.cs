using KurdStudio.AdminApi.Endpoints;

namespace KurdStudio.AdminApi.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var api = endpoints.MapGroup("/api");

        api.MapAuthEndpoints();
        api.MapDashboardEndpoints();
        api.MapArtworkEndpoints();
        api.MapWritingEndpoints();
        api.MapImageEndpoints();
        api.MapSiteEndpoints();
        api.MapNavigationEndpoints();
        api.MapContactEndpoints();

        return endpoints;
    }
}
