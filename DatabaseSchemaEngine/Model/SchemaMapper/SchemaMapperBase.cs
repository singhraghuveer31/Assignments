using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	public abstract class SchemaMapperBase : ISchemaMapper
	{
		private readonly IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;
		private readonly TargetDatabaseFrameworkValues targetDatabaseFramework;

		public SchemaMapperBase(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository, Enum.TargetDatabaseFrameworkValues targetDatabaseFramework) 
		{
			this.targetDatabaseFramework = targetDatabaseFramework;
			this.databaseSchemaGenerationRepository = databaseSchemaGenerationRepository;
		}


		public ISchemaMappingDetail? GetSchemaMappings()
		{
			var cache = SchemaMappingCache.GetCache(databaseSchemaGenerationRepository);
			ISchemaMappingDetail? schemaMappingDetail = null;
			try
			{

				schemaMappingDetail = cache.GetSchemaMappings(targetDatabaseFramework);

				if (schemaMappingDetail == null)
				{
					throw new Exception($"{Common.MappingDetailsNotFound}: {targetDatabaseFramework}");
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