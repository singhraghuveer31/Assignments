namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	public class LengthValidationRule : IValidationRule
	{
		private readonly int minLength;
		private readonly int maxLength;

		public LengthValidationRule(int minLength, int maxLength)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		public bool IsValid(string syntax)
		{
			return syntax.Length >= minLength && syntax.Length <= maxLength;
		}
	}
}
