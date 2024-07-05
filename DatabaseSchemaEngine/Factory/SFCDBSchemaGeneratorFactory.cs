using DatabaseSchemaEngine.Formatter.FormatProvider;
using DatabaseSchemaEngine.Formatter.FormatProvider.SFCDB;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.DomainModelGenerator;
using DatabaseSchemaEngine.Model.SchemaGenerator;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Repository;
using DatabaseSchemaEngine.Validator.ValidationMessage;
using DatabaseSchemaEngine.Validator.ValidatorProvider;
using DatabaseSchemaEngine.Validator.ValidatorProvider.SFCDB;
using Serilog;

namespace DatabaseSchemaEngine.Factory
{
	public class SFCDBSchemaGeneratorFactory : ISchemaGeneratorFactory
	{
		private readonly ILogger logger;

		public SFCDBSchemaGeneratorFactory(ILogger logger)
		{
			this.logger = logger;
		}
		public IDatabaseSchemaGenerator GetDatabaseSchemaGenerator()
		{
			return new SFCDBSchemaGenerator(GetSchemaGenerationRepository(), logger, GetSchemaMapper());
		}

		public ISchemaMapper GetSchemaMapper()
		{
			return new SFCDBSchemaMapper(GetSchemaGenerationRepository(), logger);
		}

		public IDatabaseSchemaGenerationRepository GetSchemaGenerationRepository()
		{
			return new DatabaseSchemaGenerationRepository();
		}

		public IValidatorProvider GetValidatorProvider() 
		{
			var validationResistration = new SFCDBSchemaValidatorProvider(GetValidationMessageProvider());
			return validationResistration;
		}

		public IValidationMessageProvider GetValidationMessageProvider()
		{
			return new SFCDBValidationMessageProvider();
		}

		public IFormatterProvider GetFormatterProvider(IEnumerable<ILookup> schemaGenerationOptions) 
		{
			var formatterProvider = new SFCDBFormatterProvider(schemaGenerationOptions);
			return formatterProvider;
		}

		public IDomainModelGenerator GetDomainModelGenerator() 
		{
			return new DomainModelGenerator();
		}
	}
}
