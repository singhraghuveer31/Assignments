using DatabaseSchemaEngine.Helper;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;

namespace DatabaseSchemaEngine.Model.SchemaMapper
{
	/// <summary>
	/// Defines methods required for schema mapping.
	/// </summary>
	public interface ISchemaMapper
	{
		ISchemaMappingDetail? GetSchemaMappings();
	}
}
