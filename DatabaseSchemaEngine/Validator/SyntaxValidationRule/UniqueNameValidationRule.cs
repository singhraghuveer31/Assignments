using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Helper.Cache;

namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	internal class UniqueNameValidationRule : IValidationRule
	{
		private readonly TypeName typeName;

		public UniqueNameValidationRule(TypeName typeName)
		{
			this.typeName = typeName;
		}

		public bool IsValid(string name)
		{
			if (typeName == TypeName.Entity && SchemaMappingCache.EntityNameValidationCache.Contains(name))
			{
				return false;
			}

			if (typeName == TypeName.Attibute && SchemaMappingCache.AttributeNameValidationCache.Contains(name)) 
			{
				return false;
			}

			return true;
		}
	}
}
