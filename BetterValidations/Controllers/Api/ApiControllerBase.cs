using CoreAbstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace BetterValidations.Controllers.Api
{
	[ServiceFilter(typeof(HttpErrorFilterAttribute))]
	public class ApiControllerBase : Controller
    {
    }

	public class HttpErrorFilterAttribute : ExceptionFilterAttribute
	{
		public HttpErrorFilterAttribute()
		{
			// Because we're setting this up as a service filter, we can access the apps IoC container
			// and use dependency injection here like we would elsewhere.
		}

		public override void OnException(ExceptionContext context)
		{
			bool exceptionHandled = HttpErrorFilterAttribute.TryHandleException(context);
			if (exceptionHandled)
			{
				return;
			}

			// at this point, some other exception was thrown that we havent explicitly handled.
			// take them to some generic error page or something
		}

		private static bool TryHandleException(ExceptionContext context)
		{
			switch(context.Exception)
			{
				case ValidationException validationException:
					foreach (KeyValuePair<string, string> error in validationException.ValidationErrors)
					{
						context.ModelState.AddModelError(error.Key, error.Value);
					}

					context.Result = new BadRequestObjectResult(context.ModelState);
					return true;
				// include other cases for other exceptions you'd like to explicitly handle globally
				default:
					return false;
			}
		}
	}
}
