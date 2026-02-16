using KurdStudio.AdminApi.Models.Shared;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface INavigationRepository
{
    Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync();
    Task<int> CreateNavigationItemAsync(NavigationItem item);
    Task UpdateNavigationItemAsync(NavigationItem item);
    Task DeleteNavigationItemAsync(int id);
    Task<IEnumerable<SocialLink>> GetSocialLinksAsync();
    Task<int> CreateSocialLinkAsync(SocialLink link);
    Task UpdateSocialLinkAsync(SocialLink link);
    Task DeleteSocialLinkAsync(int id);
}
