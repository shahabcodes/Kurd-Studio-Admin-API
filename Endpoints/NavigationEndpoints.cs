using KurdStudio.AdminApi.Models.DTOs;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Endpoints;

public static class NavigationEndpoints
{
    public static RouteGroupBuilder MapNavigationEndpoints(this RouteGroupBuilder group)
    {
        var nav = group.MapGroup("/navigation").WithTags("Navigation").RequireAuthorization();

        nav.MapGet("/", GetAll).WithName("AdminGetNavItems");
        nav.MapPost("/", Create).WithName("AdminCreateNavItem");
        nav.MapPut("/{id:int}", Update).WithName("AdminUpdateNavItem");
        nav.MapDelete("/{id:int}", Delete).WithName("AdminDeleteNavItem");

        var social = group.MapGroup("/social").WithTags("Social Links").RequireAuthorization();

        social.MapGet("/", GetSocialLinks).WithName("AdminGetSocialLinks");
        social.MapPost("/", CreateSocialLink).WithName("AdminCreateSocialLink");
        social.MapPut("/{id:int}", UpdateSocialLink).WithName("AdminUpdateSocialLink");
        social.MapDelete("/{id:int}", DeleteSocialLink).WithName("AdminDeleteSocialLink");

        return group;
    }

    // Navigation Items
    private static async Task<IResult> GetAll(INavigationRepository repository)
    {
        var items = await repository.GetNavigationItemsAsync();
        return Results.Ok(items);
    }

    private static async Task<IResult> Create(NavigationItemRequest request, INavigationRepository repository)
    {
        var item = new NavigationItem
        {
            Label = request.Label,
            Link = request.Link,
            IconSvg = request.IconSvg,
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive
        };
        var id = await repository.CreateNavigationItemAsync(item);
        return Results.Created($"/api/navigation/{id}", new { Id = id });
    }

    private static async Task<IResult> Update(int id, NavigationItemRequest request, INavigationRepository repository)
    {
        var item = new NavigationItem
        {
            Id = id,
            Label = request.Label,
            Link = request.Link,
            IconSvg = request.IconSvg,
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive
        };
        await repository.UpdateNavigationItemAsync(item);
        return Results.Ok(new { Message = "Updated" });
    }

    private static async Task<IResult> Delete(int id, INavigationRepository repository)
    {
        await repository.DeleteNavigationItemAsync(id);
        return Results.Ok(new { Message = "Deleted" });
    }

    // Social Links
    private static async Task<IResult> GetSocialLinks(INavigationRepository repository)
    {
        var links = await repository.GetSocialLinksAsync();
        return Results.Ok(links);
    }

    private static async Task<IResult> CreateSocialLink(SocialLinkRequest request, INavigationRepository repository)
    {
        var link = new SocialLink
        {
            Platform = request.Platform,
            Url = request.Url,
            IconSvg = request.IconSvg,
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive
        };
        var id = await repository.CreateSocialLinkAsync(link);
        return Results.Created($"/api/social/{id}", new { Id = id });
    }

    private static async Task<IResult> UpdateSocialLink(int id, SocialLinkRequest request, INavigationRepository repository)
    {
        var link = new SocialLink
        {
            Id = id,
            Platform = request.Platform,
            Url = request.Url,
            IconSvg = request.IconSvg,
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive
        };
        await repository.UpdateSocialLinkAsync(link);
        return Results.Ok(new { Message = "Updated" });
    }

    private static async Task<IResult> DeleteSocialLink(int id, INavigationRepository repository)
    {
        await repository.DeleteSocialLinkAsync(id);
        return Results.Ok(new { Message = "Deleted" });
    }
}
