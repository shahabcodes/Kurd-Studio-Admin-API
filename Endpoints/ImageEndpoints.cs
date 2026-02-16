using KurdStudio.AdminApi.Models.DTOs;
using KurdStudio.AdminApi.Models.Shared;
using KurdStudio.AdminApi.Repositories.Interfaces;
using SkiaSharp;

namespace KurdStudio.AdminApi.Endpoints;

public static class ImageEndpoints
{
    public static RouteGroupBuilder MapImageEndpoints(this RouteGroupBuilder group)
    {
        var images = group.MapGroup("/images").WithTags("Images").RequireAuthorization();

        images.MapGet("/", GetAll).WithName("AdminGetImages");
        images.MapGet("/{id:int}", GetImage).WithName("AdminGetImage").AllowAnonymous();
        images.MapGet("/{id:int}/thumbnail", GetThumbnail).WithName("AdminGetThumbnail").AllowAnonymous();
        images.MapPost("/upload", Upload).WithName("AdminUploadImage").DisableAntiforgery();
        images.MapPut("/{id:int}", UpdateMeta).WithName("AdminUpdateImageMeta");
        images.MapDelete("/{id:int}", Delete).WithName("AdminDeleteImage");

        return group;
    }

    private static async Task<IResult> GetAll(IImageRepository repository, HttpRequest request)
    {
        var images = await repository.GetAllMetaAsync();
        var baseUrl = $"{request.Scheme}://{request.Host}";

        var dtos = images.Select(img => new ImageMetaDto(
            img.Id, img.FileName, img.ContentType, img.AltText,
            img.FileSize, img.Width, img.Height,
            $"{baseUrl}/api/images/{img.Id}",
            $"{baseUrl}/api/images/{img.Id}/thumbnail",
            img.CreatedAt
        ));

        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetImage(int id, IImageRepository repository, HttpContext context)
    {
        var image = await repository.GetByIdAsync(id);
        if (image?.ImageData == null) return Results.NotFound();

        context.Response.Headers.CacheControl = "public, max-age=31536000";
        return Results.Bytes(image.ImageData, image.ContentType);
    }

    private static async Task<IResult> GetThumbnail(int id, IImageRepository repository, HttpContext context)
    {
        var image = await repository.GetThumbnailByIdAsync(id);
        if (image?.ImageData == null && image?.ThumbnailData == null) return Results.NotFound();

        context.Response.Headers.CacheControl = "public, max-age=31536000";
        var data = image.ThumbnailData ?? image.ImageData;
        return Results.Bytes(data!, image.ContentType);
    }

    private static async Task<IResult> Upload(
        IFormFile file,
        IImageRepository repository,
        string? altText = null)
    {
        if (file == null || file.Length == 0)
            return Results.BadRequest(new { Error = "No file provided" });

        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType))
            return Results.BadRequest(new { Error = "Only JPEG, PNG, GIF, and WebP images are allowed" });

        if (file.Length > 20 * 1024 * 1024)
            return Results.BadRequest(new { Error = "File size cannot exceed 20MB" });

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var imageData = memoryStream.ToArray();

        // Generate thumbnail using SkiaSharp
        byte[]? thumbnailData = null;
        int width = 0, height = 0;

        try
        {
            using var skBitmap = SKBitmap.Decode(imageData);
            if (skBitmap != null)
            {
                width = skBitmap.Width;
                height = skBitmap.Height;

                // Generate thumbnail (400px width)
                var thumbnailWidth = Math.Min(400, skBitmap.Width);
                var thumbnailHeight = (int)((float)skBitmap.Height / skBitmap.Width * thumbnailWidth);

                using var resized = skBitmap.Resize(new SKImageInfo(thumbnailWidth, thumbnailHeight), SKSamplingOptions.Default);
                if (resized != null)
                {
                    using var skImage = SKImage.FromBitmap(resized);
                    using var encoded = skImage.Encode(SKEncodedImageFormat.Jpeg, 75);
                    thumbnailData = encoded.ToArray();
                }
            }
        }
        catch
        {
            // If thumbnail generation fails, continue without thumbnail
        }

        var image = new Image
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            ImageData = imageData,
            ThumbnailData = thumbnailData,
            AltText = altText,
            FileSize = (int)file.Length,
            Width = width,
            Height = height
        };

        var id = await repository.UploadAsync(image);
        return Results.Created($"/api/images/{id}", new { Id = id });
    }

    private static async Task<IResult> UpdateMeta(
        int id,
        ImageMetaUpdateRequest request,
        IImageRepository repository)
    {
        var existing = await repository.GetMetaByIdAsync(id);
        if (existing == null) return Results.NotFound();

        await repository.UpdateMetaAsync(id, request.FileName, request.AltText);
        return Results.Ok(new { Message = "Updated" });
    }

    private static async Task<IResult> Delete(int id, IImageRepository repository)
    {
        var existing = await repository.GetMetaByIdAsync(id);
        if (existing == null) return Results.NotFound();

        try
        {
            await repository.DeleteAsync(id);
            return Results.Ok(new { Message = "Deleted" });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { Error = ex.Message });
        }
    }
}

public record ImageMetaUpdateRequest(string FileName, string? AltText);
