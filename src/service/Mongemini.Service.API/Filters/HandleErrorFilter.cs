using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mongemini.Persistence.Contracts.Exceptions;
using Mongemini.Service.API.Filters.Errors;
using System.Net;

namespace Mongemini.Service.API.Filters
{
    public class HandleErrorFilter : ExceptionFilterAttribute
    {
        private const string UnhandledException = "При обработки запроса произошла ошибка: {0}";

        private readonly ILogger logger;
        private readonly bool isDevelopment;

        public HandleErrorFilter(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
        {
            isDevelopment = environment.IsDevelopment();
            logger = loggerFactory.CreateLogger<HandleErrorFilter>();
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            switch (context.Exception)
            {
                case NotFoundException _:
                    SetNotFoundResult(context);
                    break;
                case UnauthorizedAccessException _:
                    SetUnauthorizedResult(context);
                    break;
                case ValidationException ve:
                    SetBadRequestResult(context, ve);
                    break;
                default:
                    SetExceptionResult(context);
                    break;
            }

            context.ExceptionHandled = true;
        }

        private static void SetUnauthorizedResult(ExceptionContext context)
        {
            context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }

        private static void SetNotFoundResult(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(
                new ErrorViewModel
                {
                    Code = context.HttpContext.TraceIdentifier,
                    Message = context.Exception.Message,
                });
        }

        private static void SetBadRequestResult(ExceptionContext context, ValidationException exception)
        {
            context.Result = new BadRequestObjectResult(new ValidationResultViewModel(exception));
        }

        private void SetExceptionResult(ExceptionContext context)
        {
            logger.LogError(context.Exception, message: string.Format(UnhandledException, context.HttpContext.TraceIdentifier));
            var result = new ObjectResult(new GenericErrorViewModel(context, isDevelopment))
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.Result = result;
        }
    }
}
