using DatabaseSchemaEngine.Repository;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	/// <summary>
	/// Handles schema mappings for SFCDB.
	/// </summary>
	public class SQLiteSchemaMapper : SchemaMapperBase
	{
		public SQLiteSchemaMapper(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository) :base(databaseSchemaGenerationRepository, Enum.TargetDatabaseFrameworkValues.SQLite)
		{
		}
	}
}
