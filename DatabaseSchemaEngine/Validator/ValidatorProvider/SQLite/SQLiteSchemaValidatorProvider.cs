using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider.SFCDB
{
	internal class SQLiteSchemaValidatorProvider : SchemaValidatorProviderBase
	{
		public SQLiteSchemaValidatorProvider(IValidationMessageProvider validationMessageProvider) : base(validationMessageProvider)
		{
		}

		protected override void Register()
		{
		}
	}
}
