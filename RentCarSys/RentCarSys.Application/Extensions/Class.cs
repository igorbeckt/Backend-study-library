using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class RemoveIdPropertyFilter : IOpenApiSchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties.ContainsKey("Id"))
        {
            schema.Properties.Remove("Id");
        }
    }
}
