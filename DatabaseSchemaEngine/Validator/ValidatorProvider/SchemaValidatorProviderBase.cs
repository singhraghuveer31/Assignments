using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider
{
	abstract class SchemaValidatorProviderBase : IValidatorProvider
	{
		private readonly IValidationMessageProvider validationMessageProvider;

		public SchemaValidatorProviderBase(IValidationMessageProvider validationMessageProvider)
		{
			this.validationMessageProvider = validationMessageProvider;
			Register();
		}

		protected abstract void Register();

		public List<string> Validate(List<IEntityDetail> entityDetails)
		{
			SchemaMappingCache.EntityNameValidationCache.Clear();

			var errorList = new List<string>();
			foreach (var entity in entityDetails)
			{
				entity.Validate(errorList, validationMessageProvider);

				SchemaMappingCache.AttributeNameValidationCache.Clear();
				foreach (var attribute in entity.Attributes)
				{
					attribute.Validate(errorList, validationMessageProvider);
				}
			}

			return errorList;
		}
	}
}