namespace KurdStudio.AdminApi.Models.Shared;

public class Writing
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int WritingTypeId { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string TypeDisplayName { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Excerpt { get; set; }
    public string? FullContent { get; set; }
    public DateTime? DatePublished { get; set; }
    public string? NovelName { get; set; }
    public int? ChapterNumber { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
