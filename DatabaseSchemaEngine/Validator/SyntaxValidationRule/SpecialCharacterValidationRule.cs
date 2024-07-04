
using System.Text.RegularExpressions;

namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	internal class SpecialCharacterValidationRule : IValidationRule
	{
		private readonly List<char> allowedSpecialChars;

		public SpecialCharacterValidationRule(List<char> allowedSpecialChars)
		{
			this.allowedSpecialChars = allowedSpecialChars;
		}

		public bool IsValid(string syntax)
		{
			var chars = string.Join("", allowedSpecialChars);
			return new Regex($"^[a-zA-Z0-9{chars}]").IsMatch(syntax);
		}
	}
}
