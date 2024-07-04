
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator
{
	public interface IValidatable
	{
		bool Validate(IValidationRule rule, IValidationMessageProvider validationMessage, out string errorMessage);
	}
}
