using System.Web.Http.ModelBinding;
using Scrummage.Core.Services.Validation;
namespace Scrummage.Services.Validation
{
    public class ValidationDictionaryWebApi : IValidationDictionary
    {
        private readonly ModelStateDictionary _modelState;

        public ValidationDictionaryWebApi(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid => _modelState.IsValid;
    }
}