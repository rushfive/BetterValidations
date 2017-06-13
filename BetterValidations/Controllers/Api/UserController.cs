using BetterValidations.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAbstractions.Services;
using System;
using System.Threading.Tasks;

namespace BetterValidations.Controllers.Api
{
	[Route("api/User")]
    public class UserController : ApiControllerBase
	{
		private IUserService userService { get; }

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Add([FromBody]UserAddModel model)
		{
			var entity = UserAddModel.FromModel(model);

			Guid id = await this.userService.AddAsync(entity);

			return this.Ok(id);
		}
    }
}
