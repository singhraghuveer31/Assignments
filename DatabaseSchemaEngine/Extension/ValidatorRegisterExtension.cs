using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Validator;

namespace DatabaseSchemaEngine.Extension
{
	public static class ValidatorRegisterExtension
	{
		public static void RegisterValidator(string typeName, IValidationRule rule)
		{
			Validator.Validator.RegisterValidatorFor(typeName, rule);
		}
	}
}
