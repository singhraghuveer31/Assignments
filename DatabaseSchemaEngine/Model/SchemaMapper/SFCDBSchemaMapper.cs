using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	/// <summary>
	/// Handles schema mappings for SFCDB.
	/// </summary>
	public class SFCDBSchemaMapper : ISchemaMapper
	{
		private readonly IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;

		public SFCDBSchemaMapper(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository)
		{
			this.databaseSchemaGenerationRepository = databaseSchemaGenerationRepository;
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
					throw new Exception( $"{Common.MappingDetailsNotFound}: { Enum.TargetDatabaseFrameworkValues.SFCDB}");
				}
			}
			catch
			{
				throw;
			}

			return schemaMappingDetail;
		}
	}
}
