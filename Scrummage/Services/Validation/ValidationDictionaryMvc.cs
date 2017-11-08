using System.Web.Mvc;
using Scrummage.Core.Services.Validation;
namespace Scrummage.Services.Validation
{
    public class ValidationDictionaryMvc : IValidationDictionary
    {
        private readonly ModelStateDictionary _modelState;

        public ValidationDictionaryMvc(ModelStateDictionary modelState)
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