namespace DatabaseSchemaEngine.Validator
{
	public static class Validator
	{
		private static Dictionary<Type, List<IValidationRule>> _validators = new Dictionary<Type, List<IValidationRule>>();

		public static void RegisterValidatorFor<T>(T entity, IValidationRule validator)
			where T : IValidatable
		{
			if (_validators.ContainsKey(entity.GetType()))
			{
				_validators[entity.GetType()].Add(validator);
			}
			else 
			{
				_validators.Add(entity.GetType(), new List<IValidationRule> { validator });
			}
		}

		public static List<IValidationRule> GetValidatorsFor<T>(T entity)
			where T : IValidatable
		{
			return _validators[entity.GetType()];
		}

		public static void Validate<T>(this T entity, List<string> errors)
			where T : IValidatable
		{
			if (errors == null) 
			{
				errors = new List<string>();
			}

			List<IValidationRule> validatioRules = GetValidatorsFor(entity);
			foreach (var rule in validatioRules) 
			{
				if (!rule.IsValid()) 
				{
					errors.AddRange(rule.Erros.ToList());
				}
			}
		}
	}
}
