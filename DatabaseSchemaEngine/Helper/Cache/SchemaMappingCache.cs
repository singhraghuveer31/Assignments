using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;

namespace DatabaseSchemaEngine.Helper.Cache
{
	public sealed class SchemaMappingCache
	{
		public static HashSet<string> EntityNameValidationCache = new HashSet<string>();
		public static HashSet<string> AttributeNameValidationCache = new HashSet<string>();

		private static SchemaMappingCache? schemaMappingCache;
		private static Dictionary<string, SchemaMappingDetail>? schemaMappings;
		static object obj = new object();

		private SchemaMappingCache(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository)
		{
			schemaMappings = databaseSchemaGenerationRepository.GetSchemaMappings();
		}

		public static SchemaMappingCache GetCache(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository) 
		{
			if (schemaMappingCache == null) 
			{
				lock (obj) 
				{
					if (schemaMappingCache == null) 
					{
						schemaMappingCache = new SchemaMappingCache(databaseSchemaGenerationRepository);
					}
				}
			}
			return schemaMappingCache;
		}

		public SchemaMappingDetail? GetSchemaMappings(Enum.TargetDatabaseFrameworkValues targetFramework) 
		{
			if(schemaMappings != null && schemaMappings.ContainsKey(targetFramework.ToString()))
				return schemaMappings[targetFramework.ToString()];

			return null;
		}

	}
}
