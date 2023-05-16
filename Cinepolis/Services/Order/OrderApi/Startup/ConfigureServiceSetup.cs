using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace OrderApi.Startup
{
    public static class ConfigureServiceSetup
    {
        public static IServiceCollection ConfigCors(this IServiceCollection services, string corsPolicy)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicy, policy =>
                {
                    policy.AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .WithOrigins("http://localhost:5173")
                    .AllowCredentials();
                });
            });

            return services;
        }

        public static IServiceCollection ConfigAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var logger = services.BuildServiceProvider().GetRequiredService<ILogger<JwtBearerHandler>>();

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            logger.LogWarning(context.Exception, "Unauthorized request.");
                            return Task.CompletedTask;
                        },
                    };
                    options.Authority = "http://IdentityApi";
                    options.MetadataAddress = "http://IdentityApi/.well-known/openid-configuration";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",

                        ValidateAudience = false,
                        ValidIssuers = new[]
                        {
                        "https://localhost:55005", // first issuer
                        },

                    };
                });

            return services;
        }

        public static IServiceCollection ConfigAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "orderApi.fullaccess");
                });
            });


            return services;
        }
    }
}
