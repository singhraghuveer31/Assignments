namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	public class SchemaGenerationOutput : ISchemaGenerationOutput
	{

		public SchemaGenerationOutput(List<string> validationMessages, bool isSuccess)
		{
			ValidationMessages = validationMessages;
			IsSuccess = isSuccess;
		}

		public List<string> ValidationMessages { get; set; }
		public bool IsSuccess { get; set; }
	}
}
