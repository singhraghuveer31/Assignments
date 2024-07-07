using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	/// <summary>
	/// Handles schema mappings for SFCDB.
	/// </summary>
	public class SFCDBSchemaMapper : SchemaMapperBase, ISchemaMapper
	{
		public SFCDBSchemaMapper(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository) :base(databaseSchemaGenerationRepository, Enum.TargetDatabaseFrameworkValues.SFCDB)
		{
		}
	}
}
