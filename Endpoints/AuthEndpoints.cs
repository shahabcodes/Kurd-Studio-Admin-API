using FluentValidation;
using KurdStudio.AdminApi.Auth;
using KurdStudio.AdminApi.Models.DTOs;
using KurdStudio.AdminApi.Models.Entities;
using KurdStudio.AdminApi.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace KurdStudio.AdminApi.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        var auth = group.MapGroup("/auth").WithTags("Auth");

        auth.MapPost("/login", Login)
            .WithName("Login")
            .AllowAnonymous();

        auth.MapPost("/refresh", Refresh)
            .WithName("RefreshToken")
            .AllowAnonymous();

        auth.MapPost("/logout", Logout)
            .WithName("Logout");

        return group;
    }

    private static async Task<IResult> Login(
        LoginRequest request,
        IAuthRepository authRepo,
        IJwtTokenService tokenService,
        IOptions<JwtSettings> jwtSettings,
        IValidator<LoginRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return Results.BadRequest(new { Errors = errors });
        }

        var user = await authRepo.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Results.Unauthorized();
        }

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();
        var settings = jwtSettings.Value;

        await authRepo.SaveRefreshTokenAsync(
            user.Id,
            refreshToken,
            DateTime.UtcNow.AddDays(settings.RefreshTokenExpirationDays)
        );

        await authRepo.UpdateLastLoginAsync(user.Id);

        return Results.Ok(new LoginResponse(
            accessToken,
            refreshToken,
            user.Username,
            user.DisplayName ?? user.Username,
            DateTime.UtcNow.AddMinutes(settings.AccessTokenExpirationMinutes)
        ));
    }

    private static async Task<IResult> Refresh(
        RefreshRequest request,
        IAuthRepository authRepo,
        IJwtTokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        var storedToken = await authRepo.GetRefreshTokenAsync(request.RefreshToken);
        if (storedToken == null)
        {
            return Results.Unauthorized();
        }

        // Revoke the old refresh token
        await authRepo.RevokeRefreshTokenAsync(request.RefreshToken);

        // Create new tokens
        var user = new AdminUser
        {
            Id = storedToken.AdminUserId,
            Username = storedToken.Username,
            DisplayName = storedToken.DisplayName
        };

        var accessToken = tokenService.GenerateAccessToken(user);
        var newRefreshToken = tokenService.GenerateRefreshToken();
        var settings = jwtSettings.Value;

        await authRepo.SaveRefreshTokenAsync(
            user.Id,
            newRefreshToken,
            DateTime.UtcNow.AddDays(settings.RefreshTokenExpirationDays)
        );

        return Results.Ok(new LoginResponse(
            accessToken,
            newRefreshToken,
            user.Username,
            user.DisplayName ?? user.Username,
            DateTime.UtcNow.AddMinutes(settings.AccessTokenExpirationMinutes)
        ));
    }

    private static async Task<IResult> Logout(
        RefreshRequest request,
        IAuthRepository authRepo)
    {
        await authRepo.RevokeRefreshTokenAsync(request.RefreshToken);
        return Results.Ok(new { Message = "Logged out successfully" });
    }
}
