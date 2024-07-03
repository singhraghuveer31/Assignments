using DatabaseSchemaEngine.Factory;
using DatabaseSchemaEngine.Model.EntityDetail;
using Serilog;

namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
    /// <summary>
    /// Implements methods required for database schema generation.
    /// </summary>
    public class DatabaseSchemaGenerationService : IDatabaseSchemaGeneratorService
    {
        private readonly ILogger logger;

		public DatabaseSchemaGenerationService(ILogger logger)
        {
            this.logger = logger;
		}

		public void GenerateDatabaseSchema(string targetFramework, IEnumerable<IEntityDetail> entityDetails)
        {
            try
            {
				var factory = FactoryProvider.GetSchemaGeneratorFactory(targetFramework, logger);
                var generator = factory.GetDatabaseSchemaGenerator();

                generator?.GenerateDatabaseSchema(entityDetails);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Generate database schema failed");
            }
        }
    }
}
