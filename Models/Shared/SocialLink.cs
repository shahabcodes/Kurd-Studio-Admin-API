namespace KurdStudio.AdminApi.Models.Shared;

public class SocialLink
{
    public int Id { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? IconSvg { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
