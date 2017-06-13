using System;
using System.Collections.Generic;

namespace CoreAbstractions
{
	public class ValidationException : Exception
	{
		public Dictionary<string, string> ValidationErrors { get; }

		public ValidationException(string message, Dictionary<string, string> validationErrors)
			: base(message)
		{
			this.ValidationErrors = validationErrors;
		}
	}
}
