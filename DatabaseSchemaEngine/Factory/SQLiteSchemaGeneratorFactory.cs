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

namespace DatabaseSchemaEngine.Factory
{
    /// <summary>
    /// Prvides factory methods for SQLite database schema generation.
    /// </summary>
    public class SQLiteSchemaGeneratorFactory : ISchemaGeneratorFactory
    {
        public IDatabaseSchemaGenerator GetDatabaseSchemaGenerator()
        {
            return new SQLiteSchemaGenerator(GetSchemaMapper());
        }

        public ISchemaMapper GetSchemaMapper()
        {
            return new SQLiteSchemaMapper(GetSchemaGenerationRepository());
        }

        public IDatabaseSchemaGenerationRepository GetSchemaGenerationRepository()
        {
            return new DatabaseSchemaGenerationRepository();
        }

        public IValidatorProvider GetValidatorProvider()
        {
            var validationResistration = new SQLiteSchemaValidatorProvider(GetValidationMessageProvider());
            return validationResistration;
        }

        public IValidationMessageProvider GetValidationMessageProvider()
        {
            return new SQLiteValidationMessageProvider();
        }

        public IFormatterProvider GetFormatterProvider(IEnumerable<ILookup> schemaGenerationOptions)
        {
            var formatterProvider = new SFCDBFormatterProvider(schemaGenerationOptions);
            return formatterProvider;
        }

        public IDomainModelMetadataGenerator GetDomainModelGenerator()
        {
            return new DomainModelMetadataGenerator();
        }
    }
}
