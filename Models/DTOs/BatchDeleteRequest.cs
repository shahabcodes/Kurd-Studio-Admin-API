namespace KurdStudio.AdminApi.Models.DTOs;

public record BatchDeleteRequest(IEnumerable<int> Ids);
