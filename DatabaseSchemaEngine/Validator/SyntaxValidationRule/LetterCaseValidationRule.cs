
namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	internal class LetterCaseValidationRule : IValidationRule
	{
		public IEnumerable<string> Erros { get; set; }

		public bool IsValid()
		{
			throw new NotImplementedException();
		}
	}
}
