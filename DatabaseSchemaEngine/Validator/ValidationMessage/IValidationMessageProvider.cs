namespace DatabaseSchemaEngine.Validator.ValidationMessage
{
	/// <summary>
	/// Provides methods to get validation message based on the rule configured.
	/// </summary>
	public interface IValidationMessageProvider
	{
		/// <summary>
		/// Gets validation message for the rule.
		/// </summary>
		/// <typeparam name="T">Validation rule which impelements <see cref="IValidationRule"/>
		/// <param name="rule">Validation rule.</param>
		/// <param name="entityName">Name of the entity.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns>Validation message.</returns>
		string GetValidationMessage<T>(T rule, string entityName, string attributeName) where T : IValidationRule;
	}
}
