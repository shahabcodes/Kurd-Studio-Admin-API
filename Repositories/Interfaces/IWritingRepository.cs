using KurdStudio.AdminApi.Models.Shared;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface IWritingRepository
{
    Task<IEnumerable<Writing>> GetAllAsync(string? typeName = null);
    Task<Writing?> GetByIdAsync(int id);
    Task<int> CreateAsync(Writing writing);
    Task UpdateAsync(Writing writing);
    Task DeleteAsync(int id);
    Task<IEnumerable<WritingType>> GetTypesAsync();
}
