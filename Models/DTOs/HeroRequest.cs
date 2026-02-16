namespace KurdStudio.AdminApi.Models.DTOs;

public record HeroRequest(
    string? Quote,
    string? QuoteAttribution,
    string? Headline,
    string? Subheading,
    int? FeaturedImageId,
    string? BadgeText,
    string? PrimaryCtaText,
    string? PrimaryCtaLink,
    string? SecondaryCtaText,
    string? SecondaryCtaLink,
    bool IsActive
);
