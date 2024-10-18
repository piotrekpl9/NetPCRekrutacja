using System.Text;
using Application.Authentication.Abstraction;
using Application.Category.Abstraction;
using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Subcategory.Abstraction;
using Application.User.Abstraction;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Abstraction;
using Infrastructure.Authentication.Entity;
using Infrastructure.Authentication.Model;
using Infrastructure.Category;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Infrastructure.Contact;
using Infrastructure.Subcategory;
using Infrastructure.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetRequiredSection(JwtSettings.SectionName);

        services.Configure<JwtSettings>(jwtSettings);
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserRepository, UserRepository>();        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();        
        services.AddScoped<IPasswordHasher<AuthenticationData>,PasswordHasher<AuthenticationData>>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            var secret = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
           
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtSettings["Issuer"], 
                ValidAudience = jwtSettings["Audience"], 
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization(options =>
        {
            var defaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
          
            options.DefaultPolicy = defaultPolicy;
        });
        
        
        return services;
    }
}