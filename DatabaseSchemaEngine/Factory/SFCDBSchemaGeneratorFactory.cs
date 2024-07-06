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
    /// Prvides factory methods for SFCDB database schema generation.
    /// </summary>
    public class SFCDBSchemaGeneratorFactory : ISchemaGeneratorFactory
    {
        public IDatabaseSchemaGenerator GetDatabaseSchemaGenerator()
        {
            return new SFCDBSchemaGenerator(GetSchemaMapper());
        }

        public ISchemaMapper GetSchemaMapper()
        {
            return new SFCDBSchemaMapper(GetSchemaGenerationRepository());
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

        public IDomainModelMetadataGenerator GetDomainModelGenerator()
        {
            return new DomainModelMetadataGenerator();
        }
    }
}
