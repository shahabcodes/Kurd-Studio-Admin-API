using KurdStudio.AdminApi.Models.Shared;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface IArtworkRepository
{
    Task<IEnumerable<Artwork>> GetAllAsync(string? typeName = null);
    Task<Artwork?> GetByIdAsync(int id);
    Task<int> CreateAsync(Artwork artwork);
    Task UpdateAsync(Artwork artwork);
    Task DeleteAsync(int id);
    Task<IEnumerable<ArtworkType>> GetTypesAsync();
}
