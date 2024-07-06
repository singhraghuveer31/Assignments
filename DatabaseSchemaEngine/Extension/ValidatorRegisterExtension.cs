using DatabaseSchemaEngine.Validator;

namespace DatabaseSchemaEngine.Extension
{
	/// <summary>
	/// Extension methods for registering validation rules against the type name.
	/// </summary>
	public static class ValidatorRegisterExtension
	{
		public static void RegisterValidator(string typeName, IValidationRule rule)
		{
			Validator.Validator.RegisterValidatorFor(typeName, rule);
		}
	}
}
