using KurdStudio.AdminApi.Models.DTOs;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Endpoints;

public static class SiteEndpoints
{
    public static RouteGroupBuilder MapSiteEndpoints(this RouteGroupBuilder group)
    {
        var site = group.MapGroup("/site").WithTags("Site Config").RequireAuthorization();

        site.MapGet("/profile", GetProfile).WithName("AdminGetProfile");
        site.MapPut("/profile", UpdateProfile).WithName("AdminUpdateProfile");
        site.MapGet("/hero", GetHero).WithName("AdminGetHero");
        site.MapPut("/hero", UpdateHero).WithName("AdminUpdateHero");
        site.MapGet("/settings", GetSettings).WithName("AdminGetSettings");
        site.MapPut("/settings/{key}", UpdateSetting).WithName("AdminUpdateSetting");
        site.MapGet("/sections", GetSections).WithName("AdminGetSections");
        site.MapPut("/sections/{id:int}", UpdateSection).WithName("AdminUpdateSection");

        return group;
    }

    private static async Task<IResult> GetProfile(ISiteRepository repository)
    {
        var profile = await repository.GetProfileAsync();
        return profile == null ? Results.NotFound() : Results.Ok(profile);
    }

    private static async Task<IResult> UpdateProfile(ProfileRequest request, ISiteRepository repository)
    {
        var profile = new Profile
        {
            Name = request.Name,
            Tagline = request.Tagline,
            Bio = request.Bio,
            AvatarImageId = request.AvatarImageId,
            Email = request.Email,
            InstagramUrl = request.InstagramUrl,
            TwitterUrl = request.TwitterUrl,
            ArtworksCount = request.ArtworksCount,
            PoemsCount = request.PoemsCount,
            YearsExperience = request.YearsExperience
        };

        await repository.UpdateProfileAsync(profile);
        return Results.Ok(new { Message = "Profile updated" });
    }

    private static async Task<IResult> GetHero(ISiteRepository repository)
    {
        var hero = await repository.GetHeroContentAsync();
        return hero == null ? Results.NotFound() : Results.Ok(hero);
    }

    private static async Task<IResult> UpdateHero(HeroRequest request, ISiteRepository repository)
    {
        var hero = new HeroContent
        {
            Quote = request.Quote,
            QuoteAttribution = request.QuoteAttribution,
            Headline = request.Headline,
            Subheading = request.Subheading,
            FeaturedImageId = request.FeaturedImageId,
            BadgeText = request.BadgeText,
            PrimaryCtaText = request.PrimaryCtaText,
            PrimaryCtaLink = request.PrimaryCtaLink,
            SecondaryCtaText = request.SecondaryCtaText,
            SecondaryCtaLink = request.SecondaryCtaLink,
            IsActive = request.IsActive
        };

        await repository.UpdateHeroContentAsync(hero);
        return Results.Ok(new { Message = "Hero content updated" });
    }

    private static async Task<IResult> GetSettings(ISiteRepository repository)
    {
        var settings = await repository.GetSettingsAsync();
        return Results.Ok(settings);
    }

    private static async Task<IResult> UpdateSetting(string key, SiteSettingRequest request, ISiteRepository repository)
    {
        await repository.UpdateSettingAsync(key, request.SettingValue);
        return Results.Ok(new { Message = "Setting updated" });
    }

    private static async Task<IResult> GetSections(ISiteRepository repository)
    {
        var sections = await repository.GetSectionsAsync();
        return Results.Ok(sections);
    }

    private static async Task<IResult> UpdateSection(int id, SectionRequest request, ISiteRepository repository)
    {
        var section = new Section
        {
            Id = id,
            Tag = request.Tag,
            Title = request.Title,
            Subtitle = request.Subtitle,
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive
        };

        await repository.UpdateSectionAsync(section);
        return Results.Ok(new { Message = "Section updated" });
    }
}
