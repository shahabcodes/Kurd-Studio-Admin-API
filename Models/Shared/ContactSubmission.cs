namespace KurdStudio.AdminApi.Models.Shared;

public class ContactSubmission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string? Budget { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsResponded { get; set; }
}
