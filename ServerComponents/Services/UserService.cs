using ServerAbstractions.Services;
using CoreAbstractions.Models;
using System.Threading.Tasks;
using System;
using ServerAbstractions.Validators;

namespace ServerComponents.Services
{
	public class UserService : IUserService
	{
		private IUserValidator validator { get; }

		public UserService(IUserValidator validator)
		{
			this.validator = validator;
		}

		public async Task<Guid> AddAsync(UserAdd user)
		{
			await this.validator.ValidateAddAsync(user);

			// at this point, user is valid to add.

			// make db call to add, then return the new user id

			return Guid.NewGuid();
		}
	}
}
