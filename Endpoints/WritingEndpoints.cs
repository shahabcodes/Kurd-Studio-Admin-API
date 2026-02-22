using FluentValidation;
using KurdStudio.AdminApi.Models.DTOs;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Endpoints;

public static class WritingEndpoints
{
    public static RouteGroupBuilder MapWritingEndpoints(this RouteGroupBuilder group)
    {
        var writings = group.MapGroup("/writings").WithTags("Writings").RequireAuthorization();

        writings.MapGet("/", GetAll).WithName("AdminGetWritings");
        writings.MapGet("/types", GetTypes).WithName("AdminGetWritingTypes");
        writings.MapGet("/{id:int}", GetById).WithName("AdminGetWritingById");
        writings.MapPost("/", Create).WithName("AdminCreateWriting");
        writings.MapPut("/{id:int}", Update).WithName("AdminUpdateWriting");
        writings.MapDelete("/{id:int}", Delete).WithName("AdminDeleteWriting");
        writings.MapDelete("/batch", DeleteBatch).WithName("AdminDeleteWritingBatch");

        return group;
    }

    private static async Task<IResult> GetAll(IWritingRepository repository, string? type = null)
    {
        var writings = await repository.GetAllAsync(type);
        return Results.Ok(writings);
    }

    private static async Task<IResult> GetTypes(IWritingRepository repository)
    {
        var types = await repository.GetTypesAsync();
        return Results.Ok(types);
    }

    private static async Task<IResult> GetById(int id, IWritingRepository repository)
    {
        var writing = await repository.GetByIdAsync(id);
        return writing == null ? Results.NotFound() : Results.Ok(writing);
    }

    private static async Task<IResult> Create(
        WritingRequest request,
        IWritingRepository repository,
        IValidator<WritingRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return Results.BadRequest(new { Errors = errors });
        }

        var writing = new Writing
        {
            Title = request.Title,
            Slug = request.Slug,
            WritingTypeId = request.WritingTypeId,
            Subtitle = request.Subtitle,
            Excerpt = request.Excerpt,
            FullContent = request.FullContent,
            DatePublished = request.DatePublished,
            NovelName = request.NovelName,
            ChapterNumber = request.ChapterNumber,
            DisplayOrder = request.DisplayOrder
        };

        var id = await repository.CreateAsync(writing);
        return Results.Created($"/api/writings/{id}", new { Id = id });
    }

    private static async Task<IResult> Update(
        int id,
        WritingRequest request,
        IWritingRepository repository,
        IValidator<WritingRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return Results.BadRequest(new { Errors = errors });
        }

        var existing = await repository.GetByIdAsync(id);
        if (existing == null) return Results.NotFound();

        var writing = new Writing
        {
            Id = id,
            Title = request.Title,
            Slug = request.Slug,
            WritingTypeId = request.WritingTypeId,
            Subtitle = request.Subtitle,
            Excerpt = request.Excerpt,
            FullContent = request.FullContent,
            DatePublished = request.DatePublished,
            NovelName = request.NovelName,
            ChapterNumber = request.ChapterNumber,
            DisplayOrder = request.DisplayOrder
        };

        await repository.UpdateAsync(writing);
        return Results.Ok(new { Message = "Updated" });
    }

    private static async Task<IResult> Delete(int id, IWritingRepository repository)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null) return Results.NotFound();

        await repository.DeleteAsync(id);
        return Results.Ok(new { Message = "Deleted" });
    }

    private static async Task<IResult> DeleteBatch(BatchDeleteRequest request, IWritingRepository repository)
    {
        if (request.Ids == null || !request.Ids.Any())
            return Results.BadRequest(new { Message = "No IDs provided" });

        await repository.DeleteBatchAsync(request.Ids);
        return Results.Ok(new { Message = $"Deleted {request.Ids.Count()} items" });
    }
}
