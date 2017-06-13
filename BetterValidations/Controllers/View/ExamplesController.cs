using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterValidations.Controllers.View
{
	[Route("Examples")]
    public class ExamplesController : Controller
    {
		[HttpGet("Vanilla")]
		public IActionResult VanillaExample()
		{
			return this.View();
		}

		[HttpGet("Angular")]
		public IActionResult AngularExample()
		{
			return this.View();
		}
    }
}
