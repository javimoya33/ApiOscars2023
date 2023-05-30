using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ApiPeliculas.Helpers
{
    // Mapear arrays de, por ejemplo, la lista de Ids de actores que tiene una película
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if (proveedorDeValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(proveedorDeValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "valor inválido para tipo List<int>");
            }

            return Task.CompletedTask;
        }
    }
}
