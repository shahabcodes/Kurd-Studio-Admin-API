using FluentValidation;

namespace KurdStudio.AdminApi.Validators;

public class ImageUploadValidator : AbstractValidator<IFormFile>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp", "image/svg+xml"];

    private const int MaxFileSize = 20 * 1024 * 1024; // 20MB

    public ImageUploadValidator()
    {
        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage("File is empty")
            .LessThanOrEqualTo(MaxFileSize).WithMessage("File size cannot exceed 20MB");

        RuleFor(x => x.ContentType)
            .Must(ct => AllowedContentTypes.Contains(ct))
            .WithMessage("Only JPEG, PNG, GIF, WebP, and SVG images are allowed");
    }
}
