using Elibri.EF.Models;
using Elibri.Authorization.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Elibri.Authorization.Services.EmailServices;
using Elibri.Authorization.Services.ResetServices;
using Elibri.Authorization.Services.TokenServices;

namespace Elibri.Authorization.Registration
{
    public static class AuthorizationRegistrationExtensions
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Identity
            services.AddIdentity<User, IdentityRole>(op =>
            {
                op.SignIn.RequireConfirmedAccount = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireDigit = false;
                op.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<Context>()
            .AddDefaultTokenProviders();

            // Authentication
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["jwt:audience"],
                    ValidateAudience = true,
                    ValidIssuer = configuration["jwt:issuer"],
                    ValidateIssuer = true,
                };

                op.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["jwt-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IResetService, ResetService>();

            // Register EmailService with configuration values
            services.AddScoped<IEmailService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var frontendUrl = config["FrontendUrl"];
                return new EmailService(frontendUrl, config);
            });
        }
    }
}
