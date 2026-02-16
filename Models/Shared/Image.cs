namespace KurdStudio.AdminApi.Models.Shared;

public class Image
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[]? ImageData { get; set; }
    public byte[]? ThumbnailData { get; set; }
    public string? AltText { get; set; }
    public int FileSize { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public DateTime CreatedAt { get; set; }
}
