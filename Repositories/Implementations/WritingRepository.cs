using System.Data;
using Dapper;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Repositories.Implementations;

public class WritingRepository : IWritingRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public WritingRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Writing>> GetAllAsync(string? typeName = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Writing>(
            "usp_Admin_GetWritings",
            new { TypeName = typeName },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Writing?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Writing>(
            "usp_Admin_GetWritingById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CreateAsync(Writing writing)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<int>(
            "usp_Admin_CreateWriting",
            new
            {
                writing.Title,
                writing.Slug,
                writing.WritingTypeId,
                writing.Subtitle,
                writing.Excerpt,
                writing.FullContent,
                writing.DatePublished,
                writing.NovelName,
                writing.ChapterNumber,
                writing.DisplayOrder
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateAsync(Writing writing)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_UpdateWriting",
            new
            {
                writing.Id,
                writing.Title,
                writing.Slug,
                writing.WritingTypeId,
                writing.Subtitle,
                writing.Excerpt,
                writing.FullContent,
                writing.DatePublished,
                writing.NovelName,
                writing.ChapterNumber,
                writing.DisplayOrder
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "usp_Admin_DeleteWriting",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<WritingType>> GetTypesAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<WritingType>(
            "usp_GetWritingTypes",
            commandType: CommandType.StoredProcedure
        );
    }
}
