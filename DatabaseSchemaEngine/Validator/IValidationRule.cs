namespace DatabaseSchemaEngine.Validator
{
	public interface IValidationRule
	{
		bool IsValid();
		IEnumerable<string> Erros { get; set; }
	}
}
