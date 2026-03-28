using Microsoft.OpenApi;

namespace Ihugi.WebApi.Configurations.Swagger;

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
                Description = "Ihugi Messanger Backend API",
                Contact = new OpenApiContact
                {
                    Name = "Contact Email",
                    Email = "brnv.ma@gmail.com"
                }
            });

            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT authorization token. Template: 'bearer <JWTToken>'",
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement()
            {
                [new OpenApiSecuritySchemeReference("bearer", document)] = []
            });

            foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml",
                         SearchOption.AllDirectories))
            {
                options.IncludeXmlComments(name);
            }

            options.SchemaFilter<EnumSchemaFilter>();

            options.AddSignalRSwaggerGen(ssgOptions => 
            {
                ssgOptions.ScanAssemblies(typeof(Presentation.AssemblyReference).Assembly);
            });
        });

        return services;
    }
}