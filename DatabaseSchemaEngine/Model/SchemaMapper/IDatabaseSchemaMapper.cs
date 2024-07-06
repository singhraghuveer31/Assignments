using DatabaseSchemaEngine.Model.SchemaMappingDetail;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	/// <summary>
	/// Defines methods required for schema mapping.
	/// </summary>
	public interface ISchemaMapper
	{
		/// <summary>
		/// Returns schema mapping details.
		/// </summary>
		/// <returns>Object of <see cref="ISchemaMapper", which contains mapping details from Domain Model to database schema./></returns>
		ISchemaMappingDetail? GetSchemaMappings();
	}
}
