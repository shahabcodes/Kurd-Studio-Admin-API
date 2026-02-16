using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class ArtworkRepository : IArtworkRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ArtworkRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Artwork>> GetAllAsync(string? typeName = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Artwork>(
            "usp_Admin_GetArtworks",
            new { TypeName = typeName },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Artwork?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Artwork>(
            "usp_Admin_GetArtworkById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CreateAsync(Artwork artwork)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<int>(
            "usp_Admin_CreateArtwork",
            new
            {
                artwork.Title,
                artwork.Slug,
                artwork.ArtworkTypeId,
                artwork.ImageId,
                artwork.Description,
                artwork.IsFeatured,
                artwork.DisplayOrder
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateAsync(Artwork artwork)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateArtwork",
            new
            {
                artwork.Id,
                artwork.Title,
                artwork.Slug,
                artwork.ArtworkTypeId,
                artwork.ImageId,
                artwork.Description,
                artwork.IsFeatured,
                artwork.DisplayOrder
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_DeleteArtwork",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<ArtworkType>> GetTypesAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<ArtworkType>(
            "usp_GetArtworkTypes",
            commandType: CommandType.StoredProcedure
        );
    }
}
