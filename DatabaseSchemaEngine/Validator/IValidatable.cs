
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator
{
	/// <summary>
	/// This interface can be used on object which eligible for validation rules.
	/// </summary>
	public interface IValidatable
	{
		/// <summary>
		/// Validates the objects implementing this interface.
		/// </summary>
		/// <param name="rule">Rule against which the object will be validated.</param>
		/// <param name="validationMessageProvider">Validation message provider.</param>
		/// <param name="errorMessage">Error message if the validation fails</param>
		/// <returns>True, if the validation is successful, false otherwise.</returns>
		bool Validate(IValidationRule rule, IValidationMessageProvider validationMessageProvider, out string errorMessage);
	}
}
