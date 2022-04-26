using Microsoft.AspNetCore.Mvc.Filters;

namespace Mongemini.Service.API.Filters.Errors
{
    public class GenericErrorViewModel : ErrorViewModel
    {
        public GenericErrorViewModel() { }

        public GenericErrorViewModel(ExceptionContext context, bool showStackTrace)
        {
            Code = context.HttpContext.TraceIdentifier;
            Init(context.Exception, showStackTrace);
        }

        public GenericErrorViewModel(Exception ex, bool showStackTrace)
        {
            Init(ex, showStackTrace);
        }

        public GenericErrorViewModel InnerException { get; set; }

        public string[] StackTrace { get; set; }

        private void Init(Exception error, bool showStackTrace)
        {
            Message = error.Message;
            if (showStackTrace)
            {
                ParseStackTrace(error);
            }

            if (error.InnerException != null)
            {
                InnerException = new GenericErrorViewModel(error.InnerException, showStackTrace);
            }
        }

        private void ParseStackTrace(Exception exception)
        {
            StackTrace = exception.StackTrace.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
