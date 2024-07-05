using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;
using Serilog;

namespace DatabaseSchemaEngine.Model.SchemaGenerator
{
	/// <summary>
	/// Handles schema mappings for SFCDB.
	/// </summary>
	public class SFCDBSchemaMapper : ISchemaMapper
	{
		private readonly IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;
		private readonly ILogger logger;

		public SFCDBSchemaMapper(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository, ILogger logger)
		{
			this.databaseSchemaGenerationRepository = databaseSchemaGenerationRepository;
			this.logger = logger;
		}

		public ISchemaMappingDetail? GetSchemaMappings()
		{
			var cache = SchemaMappingCache.GetCache(databaseSchemaGenerationRepository);
			ISchemaMappingDetail? schemaMappingDetail = null;
			try
			{

				schemaMappingDetail = cache.GetSchemaMappings(Enum.TargetDatabaseFrameworkValues.SFCDB);

				if (schemaMappingDetail == null)
				{
					throw new Exception($"Mapping details not found for database framework: {Enum.TargetDatabaseFrameworkValues.SFCDB}");
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Mapping not found.");
			}

			return schemaMappingDetail;
		}
	}
}
