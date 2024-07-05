namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
    /// <summary>
    /// Service interface to generate database schema.
    /// </summary>
    public interface IDatabaseSchemaGeneratorService
    {
        /// <summary>
        /// Generates database schema.
        /// </summary>
        void GenerateDatabaseSchema(ISchemaGenerationInput schemaGenerationInput);

		/// <summary>
		/// Contains output for schema generation service.
		/// </summary>
		ISchemaGenerationOutput Output { get; protected set; }
    }
}
