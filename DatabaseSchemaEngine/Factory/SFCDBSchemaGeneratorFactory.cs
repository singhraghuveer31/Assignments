using DatabaseSchemaEngine.Model.SchemaGenerator;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Repository;
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
	}
}
