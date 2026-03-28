using System.ComponentModel;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ihugi.WebApi.Configurations.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum)
            return;

        foreach (var name in Enum.GetNames(context.Type))
        {
            var enumValue = (Enum)Enum.Parse(context.Type, name);
            schema.Description += $"\n\n{Convert.ToInt32(enumValue)} - {enumValue.GetAttributeOfType<DescriptionAttribute>()?.Description}";
        }
    }
}