using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator
{
	public static class Validator
	{
		private static Dictionary<string, List<IValidationRule>> _validators = new Dictionary<string, List<IValidationRule>>();

		public static void RegisterValidatorFor(string typeName, IValidationRule validator)
		{
			if (_validators.ContainsKey(typeName))
			{
				_validators[typeName].Add(validator);
			}
			else 
			{
				_validators.Add(typeName, new List<IValidationRule> { validator });
			}
		}

		public static List<IValidationRule> GetValidatorsFor(string typeName)
		{
			return _validators[typeName];
		}

		public static void Validate<T>(this T entity, List<string> errors, IValidationMessageProvider validationMessage)
			where T : IValidatable
		{
			if (errors == null) 
			{
				errors = new List<string>();
			}

			List<IValidationRule> validatioRules = GetValidatorsFor(entity.GetType().Name);
			foreach (var rule in validatioRules) 
			{
				if (!entity.Validate(rule, validationMessage, out string errorMessage)) 
				{
					errors.Add(errorMessage);
				}
			}
		}
	}
}
