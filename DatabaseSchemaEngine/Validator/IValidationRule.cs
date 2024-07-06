namespace DatabaseSchemaEngine.Validator
{
	/// <summary>
	/// Interface for validation rule.
	/// </summary>
	public interface IValidationRule
	{
		/// <summary>
		/// Validates the provided input against the validation rule.
		/// </summary>
		/// <param name="syntax">Input string to be validated.</param>
		/// <returns>true, if syntax is as per rule, false otherwise.</returns>
		bool IsValid(string syntax);
	}
}
