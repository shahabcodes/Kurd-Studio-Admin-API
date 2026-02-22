using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class ImageRepository : IImageRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ImageRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Image>> GetAllMetaAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Image>(
            "usp_Admin_GetImages",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Image?> GetMetaByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Image>(
            "usp_Admin_GetImageById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Image?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Image>(
            "usp_GetImageById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Image?> GetThumbnailByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Image>(
            "usp_GetImageThumbnailById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> UploadAsync(Image image)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<int>(
            "usp_Admin_UploadImage",
            new
            {
                image.FileName,
                image.ContentType,
                image.ImageData,
                image.ThumbnailData,
                image.AltText,
                image.FileSize,
                image.Width,
                image.Height
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateMetaAsync(int id, string fileName, string? altText)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateImageMeta",
            new { Id = id, FileName = fileName, AltText = altText },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_DeleteImage",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteBatchAsync(IEnumerable<int> ids)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "DELETE FROM Images WHERE Id IN @Ids",
            new { Ids = ids });
    }
}
