namespace KurdStudio.AdminApi.Models.Shared;

public class Section
{
    public int Id { get; set; }
    public string SectionKey { get; set; } = string.Empty;
    public string? Tag { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
}
