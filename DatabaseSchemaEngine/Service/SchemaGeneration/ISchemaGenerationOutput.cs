
namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	public interface ISchemaGenerationOutput
	{
		string DatabaseSchema { get; set; }
		string DomainModelMetaData { get; set; }
		List<string> ValidationMessages { get; set; }
	}
}