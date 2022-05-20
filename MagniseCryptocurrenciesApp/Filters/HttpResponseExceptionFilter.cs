using MagniseCryptocurrenciesApp.Common.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MagniseCryptocurrenciesApp.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null) return;

            var response = new ApiResponse()
            {
                Message = context.Exception.Message,
                Data = context.Exception.InnerException?.Message,
            };
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(response)
                {
                    StatusCode = exception.Status,
                };
            }
            else
            {
                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
