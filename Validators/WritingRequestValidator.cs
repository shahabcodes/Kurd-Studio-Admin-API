using FluentValidation;
using KurdStudio.AdminApi.Models.DTOs;

namespace KurdStudio.AdminApi.Validators;

public class WritingRequestValidator : AbstractValidator<WritingRequest>
{
    public WritingRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(255);

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required")
            .MaximumLength(255)
            .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be lowercase with hyphens only");

        RuleFor(x => x.WritingTypeId)
            .GreaterThan(0).WithMessage("Writing type is required");

        RuleFor(x => x.Subtitle)
            .MaximumLength(500).When(x => x.Subtitle != null);

        RuleFor(x => x.Excerpt)
            .MaximumLength(1000).When(x => x.Excerpt != null);

        RuleFor(x => x.NovelName)
            .MaximumLength(255).When(x => x.NovelName != null);
    }
}
