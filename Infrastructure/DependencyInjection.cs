using Application.Authentication.Abstraction;
using Application.Common.Abstraction;
using Application.User.Abstraction;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Abstraction;
using Infrastructure.Authentication.Model;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Infrastructure.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = configuration.GetRequiredSection(JwtSettings.SectionName); 

        services.Configure<JwtSettings>(jwtSettings);
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserRepository, UserRepository>();        
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();        
        services.AddScoped<IPasswordHasher<AuthenticationData>,PasswordHasher<AuthenticationData>>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}