using System;

namespace CoreAbstractions.Models
{
	public class User
	{
		public string UserName { get; set; }

		public string Email { get; set; }
	}

    public class UserAdd
    {
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
	}
}
