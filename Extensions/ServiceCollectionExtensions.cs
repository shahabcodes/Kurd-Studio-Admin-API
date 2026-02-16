using System.Text;
using FluentValidation;
using KurdStudio.AdminApi.Auth;
using KurdStudio.AdminApi.Data;
using KurdStudio.AdminApi.Repositories.Implementations;
using KurdStudio.AdminApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace KurdStudio.AdminApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Data
        services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();

        // Auth
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        // Repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IArtworkRepository, ArtworkRepository>();
        services.AddScoped<IWritingRepository, WritingRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ISiteRepository, SiteRepository>();
        services.AddScoped<INavigationRepository, NavigationRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();

        // Validators
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettings);

        var secret = jwtSettings["Secret"]
            ?? throw new InvalidOperationException("JWT Secret not configured");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:4300", "https://localhost:4300")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
