using System.Collections.Generic;
using System.Linq;

namespace CoreAbstractions
{
	public class ValidationDictionary
	{
		private Dictionary<string, List<string>> internalDictionary { get; } = new Dictionary<string, List<string>>();

		public void Add(string key, string value)
		{
			if (!this.internalDictionary.ContainsKey(key))
			{
				this.internalDictionary.Add(key, new List<string>());
			}
			this.internalDictionary[key].Add(value);
		}

		public Dictionary<string, string> GetValidationResults()
		{
			return this.internalDictionary
				.Where(i => i.Value.Any())
				.ToDictionary(i => i.Key, i => string.Join(" ", i.Value));
		}

		public void ThrowIfErrors(string message = null)
		{
			Dictionary<string, string> errors = this.GetValidationResults();

			if (errors.Any())
			{
				throw new ValidationException(message, errors);
			}
		}
	}
}
