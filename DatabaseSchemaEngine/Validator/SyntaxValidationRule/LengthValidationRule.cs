namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	public class LengthValidationRule : IValidationRule
	{
		public IEnumerable<string> Erros { get; set; }

		public bool IsValid()
		{
			throw new NotImplementedException();
		}
	}
}
