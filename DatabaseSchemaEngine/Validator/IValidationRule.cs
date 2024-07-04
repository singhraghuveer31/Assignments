namespace DatabaseSchemaEngine.Validator
{
	public interface IValidationRule
	{
		bool IsValid(string syntax);
	}
}
