using DatabaseSchemaEngine.Helper;

namespace DatabaseSchemaEngine.Model.SchemaMappingDetail
{
	public interface ISchemaMappingDetail
	{
		string ColumnTemplateFileName { get; set; }
		string ModelFileExtension { get; set; }
		string SchemaFileExtension { get; set; }
		string TableTemplateFileName { get; set; }
		Map<string, string> TypeMappings { get; set; }
	}
}