using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mongemini.Service.API.Filters.Errors
{
    public class ValidationResultViewModel : ErrorViewModel
    {
        public ValidationResultViewModel() : this((ModelStateDictionary)null) { }

        public ValidationResultViewModel(ValidationException exception)
        {
            Message = exception.Message;
            if (exception.Errors != null)
            {
                Errors = exception.Errors
                    .Select(a => new ValidationErrorViewModel(a.PropertyName, a.ErrorMessage)).ToList();
            }
        }

        public ValidationResultViewModel(ModelStateDictionary modelState)
        {
            Message = @"Validation Failed";
            if (modelState == null)
            {
                return;
            }

            Errors = new List<ValidationErrorViewModel>();
            modelState.Keys.ToList().ForEach(key =>
            {
                var messages = modelState[key].Errors.ToList().Select(x =>
                {
                    var message = x.ErrorMessage;
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        return message;
                    }

                    message = x.Exception is Newtonsoft.Json.JsonException ? $"The data in {key} field is of incorrect format" : x.Exception.Message;

                    return message;
                });
                if (!messages.Any() && modelState[key].ValidationState == ModelValidationState.Unvalidated)
                {
                    messages = new string[] { $"The data in {key} field is of incorrect format" };
                }

                messages.Distinct().ToList().ForEach(m => Errors.Add(new ValidationErrorViewModel(key, m)));
            });
        }

        public List<ValidationErrorViewModel> Errors { get; }
    }
}
