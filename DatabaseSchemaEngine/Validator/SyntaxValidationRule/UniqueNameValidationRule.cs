using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Helper.Cache;

namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	internal class UniqueNameValidationRule : IValidationRule
	{
		private readonly TypeNameValues typeName;

		public UniqueNameValidationRule(TypeNameValues typeName)
		{
			this.typeName = typeName;
		}

		public bool IsValid(string name)
		{
			if (typeName == TypeNameValues.Entity && SchemaMappingCache.EntityNameValidationCache.Contains(name))
			{
				return false;
			}

			if (typeName == TypeNameValues.Attibute && SchemaMappingCache.AttributeNameValidationCache.Contains(name)) 
			{
				return false;
			}

			return true;
		}
	}
}
