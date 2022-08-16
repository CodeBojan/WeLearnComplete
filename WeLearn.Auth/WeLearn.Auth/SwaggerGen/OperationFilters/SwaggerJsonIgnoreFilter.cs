using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeLearn.Auth.SwaggerGen.OperationFilters;

public class SwaggerJsonIgnoreFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var ignoredProperties = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties()
            .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null))
            .ToList();

        if (!ignoredProperties.Any()) return;

        foreach (var property in ignoredProperties)
        {
            operation.Parameters = operation.Parameters
                .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture))
                .ToList();
        }
    }
}
