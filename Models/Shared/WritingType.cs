namespace KurdStudio.AdminApi.Models.Shared;

public class WritingType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
