using CoreAbstractions.Models;

namespace BetterValidations.Models
{
    public class UserAddModel
    {
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public static UserAdd FromModel(UserAddModel model)
		{
			return new UserAdd
			{
				ConfirmPassword = model.ConfirmPassword,
				Email = model.Email,
				Password = model.Password,
				UserName = model.UserName
			};
		}
    }
}
