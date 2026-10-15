using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GolBet.Web.ModelBinding;

/// <summary>
/// Binds decimals accepting both "," and "." as the decimal separator.
/// With the es-CO culture the default binder reads "2.50" as 250 (dot = thousands).
/// </summary>
public class DecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == ValueProviderResult.None) return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);

        var raw = valueResult.FirstValue?.Trim();
        if (string.IsNullOrEmpty(raw)) return Task.CompletedTask;   // [Required]/nullable decide

        if (decimal.TryParse(raw.Replace(',', '.'), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out var value))
        {
            bindingContext.Result = ModelBindingResult.Success(value);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                "Debe ser un número válido (ej: 2,50).");
        }

        return Task.CompletedTask;
    }
}

public class DecimalModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.UnderlyingOrModelType;
        return type == typeof(decimal) ? new DecimalModelBinder() : null;
    }
}
