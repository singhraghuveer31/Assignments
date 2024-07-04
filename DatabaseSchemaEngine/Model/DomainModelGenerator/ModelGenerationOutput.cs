namespace DatabaseSchemaEngine.Model.DomainModelGenerator
{
	public class ModelGenerationOutput : IModelGenerationOutput
	{
		public string Content { get; set; }

		public string OutputFilePath { get; set; }
	}
}
