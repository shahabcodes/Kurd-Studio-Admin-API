namespace KurdStudio.AdminApi.Models.Shared;

public class Artwork
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int ArtworkTypeId { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string TypeDisplayName { get; set; } = string.Empty;
    public int ImageId { get; set; }
    public string? Description { get; set; }
    public bool IsFeatured { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
