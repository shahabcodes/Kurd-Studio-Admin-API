using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class SiteRepository : ISiteRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SiteRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Profile?> GetProfileAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Profile>(
            "usp_Admin_GetProfile",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateProfileAsync(Profile profile)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateProfile",
            new
            {
                profile.Name,
                profile.Tagline,
                profile.Bio,
                profile.AvatarImageId,
                profile.Email,
                profile.InstagramUrl,
                profile.TwitterUrl,
                profile.ArtworksCount,
                profile.PoemsCount,
                profile.YearsExperience
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<HeroContent?> GetHeroContentAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<HeroContent>(
            "usp_Admin_GetHeroContent",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateHeroContentAsync(HeroContent hero)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateHeroContent",
            new
            {
                hero.Quote,
                hero.QuoteAttribution,
                hero.Headline,
                hero.Subheading,
                hero.FeaturedImageId,
                hero.BadgeText,
                hero.PrimaryCtaText,
                hero.PrimaryCtaLink,
                hero.SecondaryCtaText,
                hero.SecondaryCtaLink,
                hero.IsActive
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<SiteSetting>> GetSettingsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<SiteSetting>(
            "usp_Admin_GetSiteSettings",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateSettingAsync(string key, string value)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateSiteSetting",
            new { SettingKey = key, SettingValue = value },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Section>> GetSectionsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Section>(
            "usp_Admin_GetSections",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateSectionAsync(Section section)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateSection",
            new
            {
                section.Id,
                section.Tag,
                section.Title,
                section.Subtitle,
                section.DisplayOrder,
                section.IsActive
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
