using ServerAbstractions.Validators;
using System;
using System.Collections.Generic;
using CoreAbstractions.Models;
using System.Threading.Tasks;
using CoreAbstractions;
using System.Linq;

namespace ServerComponents.Validators
{
	public class UserValidator : IUserValidator
	{
		public async Task ValidateAddAsync(UserAdd user)
		{
			var validationErrors = new ValidationDictionary();

			List<UserAdd> existingUsers = await UserValidator.GetAsync();

			if (string.IsNullOrWhiteSpace(user.UserName))
			{
				validationErrors.Add("UserName", "This field is required.");
			}
			else if (existingUsers.Any(u => string.Equals(u.UserName, user.UserName, StringComparison.OrdinalIgnoreCase)))
			{
				validationErrors.Add("UserName", "This username already exists.");
			}

			if (string.IsNullOrWhiteSpace(user.Email))
			{
				validationErrors.Add("Email", "This field is required.");
			}
			else if (existingUsers.Any(u => string.Equals(u.Email, user.Email, StringComparison.OrdinalIgnoreCase)))
			{
				validationErrors.Add("Email", "This email already exists.");
			}

			bool passwordExists = !string.IsNullOrWhiteSpace(user.Password),
				confirmPasswordExists = !string.IsNullOrWhiteSpace(user.ConfirmPassword);
			if (!passwordExists)
			{
				validationErrors.Add("Password", "This field is required.");
			}
			if (!confirmPasswordExists)
			{
				validationErrors.Add("ConfirmPassword", "This field is required.");
			}

			if (passwordExists && confirmPasswordExists
				&& !string.Equals(user.Password, user.ConfirmPassword, StringComparison.OrdinalIgnoreCase))
			{
				validationErrors.Add("ConfirmPassword", "Doesn't match with password.");
			}

			validationErrors.ThrowIfErrors();
		}

		// for validation demo purposes we're faking
		// hitting a database and returning a list of users
		private static async Task<List<UserAdd>> GetAsync()
		{
			var users = new List<UserAdd>
			{
				new UserAdd
				{
					Email = "bob@test.com",
					UserName = "Bob"
				},
				new UserAdd
				{
					Email = "mary@test.com",
					UserName = "Mary"
				},
				new UserAdd
				{
					Email = "tom@test.com",
					UserName = "Tom"
				}
			};

			return await Task.FromResult(users);
		}
	}
}
