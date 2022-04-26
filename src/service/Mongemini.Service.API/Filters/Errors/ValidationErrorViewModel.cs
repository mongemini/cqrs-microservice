namespace Mongemini.Service.API.Filters.Errors
{
    public class ValidationErrorViewModel
    {
        public ValidationErrorViewModel(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }

        public string Field { get; }

        public string Message { get; }
    }
}
