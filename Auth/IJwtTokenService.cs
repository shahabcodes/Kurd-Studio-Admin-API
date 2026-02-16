using KurdStudio.AdminApi.Models.Entities;

namespace KurdStudio.AdminApi.Auth;

public interface IJwtTokenService
{
    string GenerateAccessToken(AdminUser user);
    string GenerateRefreshToken();
}
