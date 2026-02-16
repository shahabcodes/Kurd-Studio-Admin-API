using KurdStudio.AdminApi.Models.Shared;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<ContactSubmission>> GetAllAsync(bool onlyUnread = false);
    Task MarkAsReadAsync(int id);
    Task DeleteAsync(int id);
}
