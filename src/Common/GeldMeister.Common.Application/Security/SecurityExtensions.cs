using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GeldMeister.Common.Application.Security;

public static class SecurityExtensions
{
    
 /// <summary>
     /// Configures the JWT token header and payload
     /// </summary>
     public static void AddJwtAuthentication(this WebApplicationBuilder builder)
     {
         var services = builder.Services;
         var configuration = builder.Configuration;
         
         services.AddAuthentication(o =>
         {
             o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
         }).AddJwtBearer(o =>
         {
             o.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = configuration["Jwt:Issuer"],
                 ValidAudience = configuration["Jwt:Audience"],
                 ClockSkew = TimeSpan.FromDays(28),
                 IssuerSigningKey = new SymmetricSecurityKey
                     (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
             };
             
         });
    
         services.AddAuthorization(options =>
         {
             options.AddPolicy("User",
                 policy => policy.RequireClaim(ClaimTypes.Role, "User"));
             options.AddPolicy("Admin",
                 policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
         });
     }
    
     public static void UseJwtAuthentication(this WebApplication app)
     {
         app.UseAuthentication();
         app.UseAuthorization();
     }}