using DatabaseSchemaEngine.Formatter.FormatProvider;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.DomainModelGenerator;
using DatabaseSchemaEngine.Model.SchemaGenerator;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Validator.ValidationMessage;
using DatabaseSchemaEngine.Validator.ValidatorProvider;

namespace DatabaseSchemaEngine.Factory
{
	public interface ISchemaGeneratorFactory
	{
		IDatabaseSchemaGenerator GetDatabaseSchemaGenerator();
		ISchemaMapper GetSchemaMapper();
		IValidatorProvider GetValidatorProvider();

		IValidationMessageProvider GetValidationMessageProvider();
		IFormatterProvider GetFormatterProvider(IEnumerable<ILookup> schemaGenerationOptions);
		IDomainModelGenerator GetDomainModelGenerator();
	}
}