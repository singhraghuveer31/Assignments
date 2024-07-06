using DatabaseSchemaEngine.Formatter.FormatProvider;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.DomainModelGenerator;
using DatabaseSchemaEngine.Model.SchemaGenerator;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Validator.ValidationMessage;
using DatabaseSchemaEngine.Validator.ValidatorProvider;

namespace DatabaseSchemaEngine.Factory
{
	/// <summary>
	/// Provides schema generation factory methods.
	/// </summary>
	public interface ISchemaGeneratorFactory
	{
		/// <summary>
		/// Provides database schema generator instance.
		/// </summary>
		/// <returns>Object of type <see cref="IDatabaseSchemaGenerator"/></returns>
		IDatabaseSchemaGenerator GetDatabaseSchemaGenerator();

		/// <summary>
		/// Provides schema mapper instance.
		/// </summary>
		/// <returns>Object of type <see cref="ISchemaMapper"/></returns>
		ISchemaMapper GetSchemaMapper();

		/// <summary>
		/// Provides validation provider instance.
		/// </summary>
		/// <returns>Object of type <see cref="IValidatorProvider"/></returns>
		IValidatorProvider GetValidatorProvider();

		/// <summary>
		/// Provides instance for validation message provider.
		/// </summary>
		/// <returns>Object of type <see cref="IValidationMessageProvider"/></returns>
		IValidationMessageProvider GetValidationMessageProvider();

		/// <summary>
		/// Provides instance of formatter provider.
		/// </summary>
		/// <param name="schemaGenerationOptions">Schema generation options which to be registered with formatter provider.</param>
		/// <returns>Object of type <see cref="IFormatterProvider"/></returns>
		IFormatterProvider GetFormatterProvider(IEnumerable<ILookup> schemaGenerationOptions);
		
		/// <summary>
		/// Provides instance of domain model generator.
		/// </summary>
		/// <returns>Object of <see cref="IDomainModelMetadataGenerator"/></returns>
		IDomainModelMetadataGenerator GetDomainModelGenerator();
	}
}