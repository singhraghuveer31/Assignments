namespace DatabaseSchemaEngine.Validator.ValidationMessage
{
	public interface IValidationMessageProvider
	{
		string GetValidationMessage<T>(T rule, string entityName, string attributeName) where T : IValidationRule;
	}
}
