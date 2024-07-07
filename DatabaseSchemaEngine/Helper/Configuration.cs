namespace DatabaseSchemaEngine.Helper
{
	public static class Configuration
	{
		/// <summary>
		/// Indicates if entities are allowed to update from metadata file content. 
		/// </summary>
		public static bool AllowEntityUpdateFromMetadataFile { get; }
		public static string? SQLiteDatabaseSchemaOuputDirectory { get; }
		public static string? SQLiteDatabaseSchemaTemplateDirectory { get; }

		/// <summary>
		/// Returns Log file path.
		/// </summary>
		public static string? LogPath { get; }

		/// <summary>
		/// Returns output path of domain model metadat file.
		/// </summary>
		public static string? DomainModelMetadataOutputPath { get;}

		/// <summary>
		/// Returns output directory of SFCDB database schema generation file.
		/// </summary>
		public static string? SFCDDatabaseSchemaOuputDirectory { get; }

		/// <summary>
		/// Returns the template directory path of SFCD database framework.
		/// </summary>
		public static string? SFCDDatabaseSchemaTemplateDirectory { get; }

		static Configuration() 
		{
			LogPath	= System.Configuration.ConfigurationManager.AppSettings["serilog:write-to:File.path"];
			DomainModelMetadataOutputPath = System.Configuration.ConfigurationManager.AppSettings["schema:domain-model-metadata:File.outputPath"];
			SFCDDatabaseSchemaOuputDirectory = System.Configuration.ConfigurationManager.AppSettings["schema:sfcdb:database-schema:Directory.outputPath"];
			SFCDDatabaseSchemaTemplateDirectory = System.Configuration.ConfigurationManager.AppSettings["schema:sfcdb:database-schema-template:Directory.path"];
			bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["schema:domain-model:allowEnityUpdateFromFile"], out bool allowEntityUpdate);
			AllowEntityUpdateFromMetadataFile = allowEntityUpdate;

			SQLiteDatabaseSchemaOuputDirectory = System.Configuration.ConfigurationManager.AppSettings["schema:sqlite:database-schema:Directory.outputPath"];
			SQLiteDatabaseSchemaTemplateDirectory = System.Configuration.ConfigurationManager.AppSettings["schema:sqlite:database-schema-template:Directory.path"];
		}
	}
}
