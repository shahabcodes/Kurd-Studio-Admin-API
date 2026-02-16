namespace KurdStudio.AdminApi.Models.Shared;

public class HeroContent
{
    public int Id { get; set; }
    public string? Quote { get; set; }
    public string? QuoteAttribution { get; set; }
    public string? Headline { get; set; }
    public string? Subheading { get; set; }
    public int? FeaturedImageId { get; set; }
    public string? BadgeText { get; set; }
    public string? PrimaryCtaText { get; set; }
    public string? PrimaryCtaLink { get; set; }
    public string? SecondaryCtaText { get; set; }
    public string? SecondaryCtaLink { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
}
