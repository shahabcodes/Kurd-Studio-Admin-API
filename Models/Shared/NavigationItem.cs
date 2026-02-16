namespace KurdStudio.AdminApi.Models.Shared;

public class NavigationItem
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string? IconSvg { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
