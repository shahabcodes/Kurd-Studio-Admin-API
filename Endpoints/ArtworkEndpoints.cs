using FluentValidation;
using KurdStudio.AdminApi.Models.DTOs;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Endpoints;

public static class ArtworkEndpoints
{
    public static RouteGroupBuilder MapArtworkEndpoints(this RouteGroupBuilder group)
    {
        var artworks = group.MapGroup("/artworks").WithTags("Artworks").RequireAuthorization();

        artworks.MapGet("/", GetAll).WithName("AdminGetArtworks");
        artworks.MapGet("/types", GetTypes).WithName("AdminGetArtworkTypes");
        artworks.MapGet("/{id:int}", GetById).WithName("AdminGetArtworkById");
        artworks.MapPost("/", Create).WithName("AdminCreateArtwork");
        artworks.MapPut("/{id:int}", Update).WithName("AdminUpdateArtwork");
        artworks.MapDelete("/{id:int}", Delete).WithName("AdminDeleteArtwork");
        artworks.MapDelete("/batch", DeleteBatch).WithName("AdminDeleteArtworkBatch");

        return group;
    }

    private static async Task<IResult> GetAll(IArtworkRepository repository, string? type = null)
    {
        var artworks = await repository.GetAllAsync(type);
        return Results.Ok(artworks);
    }

    private static async Task<IResult> GetTypes(IArtworkRepository repository)
    {
        var types = await repository.GetTypesAsync();
        return Results.Ok(types);
    }

    private static async Task<IResult> GetById(int id, IArtworkRepository repository)
    {
        var artwork = await repository.GetByIdAsync(id);
        return artwork == null ? Results.NotFound() : Results.Ok(artwork);
    }

    private static async Task<IResult> Create(
        ArtworkRequest request,
        IArtworkRepository repository,
        IValidator<ArtworkRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return Results.BadRequest(new { Errors = errors });
        }

        var artwork = new Artwork
        {
            Title = request.Title,
            Slug = request.Slug,
            ArtworkTypeId = request.ArtworkTypeId,
            ImageId = request.ImageId,
            Description = request.Description,
            IsFeatured = request.IsFeatured,
            DisplayOrder = request.DisplayOrder
        };

        var id = await repository.CreateAsync(artwork);
        return Results.Created($"/api/artworks/{id}", new { Id = id });
    }

    private static async Task<IResult> Update(
        int id,
        ArtworkRequest request,
        IArtworkRepository repository,
        IValidator<ArtworkRequest> validator)
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

        var artwork = new Artwork
        {
            Id = id,
            Title = request.Title,
            Slug = request.Slug,
            ArtworkTypeId = request.ArtworkTypeId,
            ImageId = request.ImageId,
            Description = request.Description,
            IsFeatured = request.IsFeatured,
            DisplayOrder = request.DisplayOrder
        };

        await repository.UpdateAsync(artwork);
        return Results.Ok(new { Message = "Updated" });
    }

    private static async Task<IResult> Delete(int id, IArtworkRepository repository)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null) return Results.NotFound();

        await repository.DeleteAsync(id);
        return Results.Ok(new { Message = "Deleted" });
    }

    private static async Task<IResult> DeleteBatch(BatchDeleteRequest request, IArtworkRepository repository)
    {
        if (request.Ids == null || !request.Ids.Any())
            return Results.BadRequest(new { Message = "No IDs provided" });

        await repository.DeleteBatchAsync(request.Ids);
        return Results.Ok(new { Message = $"Deleted {request.Ids.Count()} items" });
    }
}
