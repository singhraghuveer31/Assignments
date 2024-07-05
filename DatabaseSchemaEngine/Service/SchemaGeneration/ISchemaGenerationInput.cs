using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	public interface ISchemaGenerationInput
	{
		IEnumerable<IEntityDetail> EntityDetails { get; set; }
		IEnumerable<ILookup> SchemaGenerationOptions { get; set; }
		TargetDatabaseFrameworkValues TargetFramework { get; set; }
	}
}