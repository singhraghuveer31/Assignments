namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	public class SchemaGenerationOutput : ISchemaGenerationOutput
	{

		public SchemaGenerationOutput(string domainModelMetaData, string databaseSchema, List<string> validationMessages)
		{
			DomainModelMetaData = domainModelMetaData;
			DatabaseSchema = databaseSchema;
			ValidationMessages = validationMessages;
		}

		public string DomainModelMetaData { get; set; }

		public string DatabaseSchema { get; set; }

		public List<string> ValidationMessages { get; set; }
	}
}
