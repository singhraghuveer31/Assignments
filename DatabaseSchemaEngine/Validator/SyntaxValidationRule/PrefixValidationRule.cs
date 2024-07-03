
namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	public class PrefixValidationRule : IValidationRule
	{
		public IEnumerable<string> Erros { get; set; }

		public bool IsValid()
		{
			throw new NotImplementedException();
		}
	}
}
