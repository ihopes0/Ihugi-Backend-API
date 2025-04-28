using System.ComponentModel;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ihugi.WebApi.Config;

/// <summary>
/// Фильтр для формирования документации Enum к Swagger
/// </summary>
public class EnumSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Применить филььтр
    /// </summary>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
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