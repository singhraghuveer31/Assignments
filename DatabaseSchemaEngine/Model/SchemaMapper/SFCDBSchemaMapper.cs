using DatabaseSchemaEngine.Repository;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	/// <summary>
	/// Handles schema mappings for SFCDB.
	/// </summary>
	public class SFCDBSchemaMapper : SchemaMapperBase
	{
		public SFCDBSchemaMapper(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository) :base(databaseSchemaGenerationRepository, Enum.TargetDatabaseFrameworkValues.SFCDB)
		{
		}
	}
}
