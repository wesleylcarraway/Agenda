using Flurl.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agenda.MVC.Filters
{
    public class ApplicationExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FlurlHttpException)
            {
                var exception = context.Exception as FlurlHttpException;
                if (exception.StatusCode == 401)
                {
                    context.HttpContext.SignOutAsync().GetAwaiter().GetResult();
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                }
            }

        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}
