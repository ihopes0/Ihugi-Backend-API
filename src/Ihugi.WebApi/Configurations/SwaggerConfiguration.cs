using Ihugi.WebApi.Config;
using Microsoft.OpenApi.Models;

namespace Ihugi.WebApi.Configurations;

internal static class SwaggerConfiguration
{
    public static IServiceCollection AddConfiguredSwagger(
        this IServiceCollection services
    )
    {
        services.AddSwaggerGen(static options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "API Ihugi",
                Description = "Сервис для приложения чата Ihugi",
                Contact = new OpenApiContact
                {
                    Name = "Email главного разработчика",
                    Email = "brnv.ma@gmail.com"
                }
            });

            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT токен авторизации"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                        { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" } },
                    new List<string>()
                }
            });

            foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml",
                         SearchOption.AllDirectories))
            {
                options.IncludeXmlComments(name);
            }

            options.SchemaFilter<EnumSchemaFilter>();
        });

        return services;
    }
}