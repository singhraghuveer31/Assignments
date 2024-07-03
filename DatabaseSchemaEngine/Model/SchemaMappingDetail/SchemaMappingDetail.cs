using DatabaseSchemaEngine.Helper;

namespace DatabaseSchemaEngine.Model.SchemaMappingDetail
{
	public class SchemaMappingDetail : ISchemaMappingDetail
	{
		public Map<string, string> TypeMappings { get; set; }

		public SchemaMappingDetail(Map<string, string> typeMappings, string tabelTemplateFileName, string columnTemplateFileName, string schemaFileExtension, string modelFileExtension)
		{
			TypeMappings = typeMappings;
			TableTemplateFileName = tabelTemplateFileName;
			ColumnTemplateFileName = columnTemplateFileName;
			SchemaFileExtension = schemaFileExtension;
			ModelFileExtension = modelFileExtension;
		}

		public string TableTemplateFileName { get; set; }
		public string ColumnTemplateFileName { get; set; }
		public string SchemaFileExtension { get; set; }
		public string ModelFileExtension { get; set; }
	}
}
