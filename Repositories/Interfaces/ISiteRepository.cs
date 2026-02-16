using KurdStudio.AdminApi.Models.Shared;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface ISiteRepository
{
    Task<Profile?> GetProfileAsync();
    Task UpdateProfileAsync(Profile profile);
    Task<HeroContent?> GetHeroContentAsync();
    Task UpdateHeroContentAsync(HeroContent hero);
    Task<IEnumerable<SiteSetting>> GetSettingsAsync();
    Task UpdateSettingAsync(string key, string value);
    Task<IEnumerable<Section>> GetSectionsAsync();
    Task UpdateSectionAsync(Section section);
}
