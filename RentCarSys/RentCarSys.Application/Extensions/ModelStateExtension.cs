using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Linq;

namespace Localdorateste.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> PegarErros(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var ModelStateEntry in modelState.Values)
                result.AddRange(ModelStateEntry.Errors.Select(error => error.ErrorMessage));



            return result;
        }
    }
}
