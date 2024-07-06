
using System.Text.RegularExpressions;

namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	public class SpecialCharacterValidationRule : IValidationRule
	{
		private readonly List<char> allowedSpecialChars;

		public SpecialCharacterValidationRule(List<char> allowedSpecialChars)
		{
			this.allowedSpecialChars = allowedSpecialChars;
		}

		public bool IsValid(string syntax)
		{
			var chars = string.Join("", allowedSpecialChars);
			return new Regex($"^[ A-Za-z0-9{chars}]*$").IsMatch(syntax);
		}
	}
}
