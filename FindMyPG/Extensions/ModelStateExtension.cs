using FindMyPG.Models.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FindMyPG.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys
                .SelectMany(k => modelState[k].Errors.Select(x => new ValidationError(k, x.ErrorMessage)))
                .ToList();
        }
    }
}
