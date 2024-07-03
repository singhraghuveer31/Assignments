using DatabaseSchemaEngine.Model.SchemaMappingDetail;

namespace DatabaseSchemaEngine.Repository
{
	/// <summary>
	/// Repository for database schema generation.
	/// </summary>
	public interface IDatabaseSchemaGenerationRepository
	{
		/// <summary>
		/// Gets available schema mappings.
		/// </summary>
		/// <returns></returns>
		Dictionary<string, SchemaMappingDetail> GetSchemaMappings();
	}
}
