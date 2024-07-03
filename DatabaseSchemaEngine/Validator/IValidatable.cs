namespace DatabaseSchemaEngine.Validator
{
	public interface IValidatable
	{
		bool Validate(IValidationRule validator, out IEnumerable<string> errors);
	}
}
