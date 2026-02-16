using FluentValidation;
using KurdStudio.AdminApi.Models.DTOs;

namespace KurdStudio.AdminApi.Validators;

public class ArtworkRequestValidator : AbstractValidator<ArtworkRequest>
{
    public ArtworkRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(255);

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required")
            .MaximumLength(255)
            .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be lowercase with hyphens only");

        RuleFor(x => x.ArtworkTypeId)
            .GreaterThan(0).WithMessage("Artwork type is required");

        RuleFor(x => x.ImageId)
            .GreaterThan(0).WithMessage("Image is required");
    }
}
