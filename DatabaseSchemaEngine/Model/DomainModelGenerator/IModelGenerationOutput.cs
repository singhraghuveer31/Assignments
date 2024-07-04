namespace DatabaseSchemaEngine.Model.DomainModelGenerator
{
	public interface IModelGenerationOutput
	{
		string Content { get; set; }
		string OutputFilePath { get; set; }
	}
}