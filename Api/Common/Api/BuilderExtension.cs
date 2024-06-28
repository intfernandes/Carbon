
using Api.DbContext;
using Api.Handlers;
using Core;
using Core.Handlers; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Api.Common.Api;

public static class BuildExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        { 
            options.SwaggerDoc("v1", new OpenApiInfo() {
                Title = "Auth",
                Version= "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                In = ParameterLocation.Header,
                Description= "Enter User Access Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat= "JWT",
                Scheme = "bearer",
            }   );

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                            }
                    },
                    []
                }
                
            });


            options.CustomSchemaIds(n => n.FullName);

             });
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<AuthDbContext>(x =>
            { x.UseNpgsql (ApiConfiguration.ConnectionString); });

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<AuthDbContext>();

    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddTransient<ICategoryHandler, CategoryHandler>();

        builder
            .Services
            .AddTransient<ITransactionHandler, TransactionHandler>();
    }
}