using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Helper.Cache;

namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	public class UniqueNameValidationRule : IValidationRule
	{
		private readonly TypeNameValues typeName;

		public UniqueNameValidationRule(TypeNameValues typeName)
		{
			this.typeName = typeName;
		}

		public bool IsValid(string name)
		{
			if (typeName == TypeNameValues.Entity)
			{
				if (SchemaMappingCache.EntityNameValidationCache.Contains(name))
				{
					return false;
				}
				else
				{
					SchemaMappingCache.EntityNameValidationCache.Add(name);
				}
			}

			if (typeName == TypeNameValues.Attibute)
			{
				if (SchemaMappingCache.AttributeNameValidationCache.Contains(name))
				{
					return false;
				}
				else 
				{
					SchemaMappingCache.AttributeNameValidationCache.Add(name);
				}
			}

			return true;
		}
	}
}
