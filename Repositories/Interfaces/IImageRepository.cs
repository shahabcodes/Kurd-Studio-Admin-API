using KurdStudio.AdminApi.Models.Shared;

namespace KurdStudio.AdminApi.Repositories.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<Image>> GetAllMetaAsync();
    Task<Image?> GetMetaByIdAsync(int id);
    Task<Image?> GetByIdAsync(int id);
    Task<Image?> GetThumbnailByIdAsync(int id);
    Task<int> UploadAsync(Image image);
    Task UpdateMetaAsync(int id, string fileName, string? altText);
    Task DeleteAsync(int id);
}
