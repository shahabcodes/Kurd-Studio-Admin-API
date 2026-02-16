namespace KurdStudio.AdminApi.Models.DTOs;

public record WritingRequest(
    string Title,
    string Slug,
    int WritingTypeId,
    string? Subtitle,
    string? Excerpt,
    string? FullContent,
    DateTime? DatePublished,
    string? NovelName,
    int? ChapterNumber,
    int DisplayOrder
);
