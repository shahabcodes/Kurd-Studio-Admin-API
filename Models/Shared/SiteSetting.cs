namespace KurdStudio.AdminApi.Models.Shared;

public class SiteSetting
{
    public int Id { get; set; }
    public string SettingKey { get; set; } = string.Empty;
    public string? SettingValue { get; set; }
    public string SettingType { get; set; } = "string";
    public DateTime UpdatedAt { get; set; }
}
