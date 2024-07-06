using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	public class SchemaGenerationInput : ISchemaGenerationInput
	{
		public SchemaGenerationInput(TargetDatabaseFrameworkValues targetFramework, IEnumerable<IEntityDetail> entityDetails, IEnumerable<ILookup> schemaGenerationOptions)
		{
			TargetFramework = targetFramework;
			EntityDetails = entityDetails;
			SchemaGenerationOptions = schemaGenerationOptions;
		}

		public TargetDatabaseFrameworkValues TargetFramework { get; set; }
		public IEnumerable<IEntityDetail> EntityDetails { get; set; }
		public IEnumerable<ILookup> SchemaGenerationOptions { get; set; }
	}
}
