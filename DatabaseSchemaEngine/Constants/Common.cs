namespace DatabaseSchemaEngine.Constants
{
	public static class Common
	{
		/// <summary>
		/// Defines the output path for domain model metadata.
		/// </summary>
		public const string DomainModelMetadataOutputPath = "C:\\DomainModelGenerationOutput\\";

		/// <summary>
		/// Defines the file name for domain model metadata.
		/// </summary>
		public const string DomainModelMetadataFileName = "DomainModel.metadata";

		/// <summary>
		/// Constant for mapping details not found.
		/// </summary>
		public const string MappingDetailsNotFound = "Mapping details not found for database framework";

		/// <summary>
		/// Constant for Invalid config for tempalate directory.
		/// </summary>
		public const string InvalidConfigForTemplatePath = "Invalid configuration for schema mapping file. File not found at path";

		/// <summary>
		/// Error message for missing mapping.
		/// </summary>
		public const string DataTypeMappingMissing = "Mapping not found for type: {0}, property: {1} in entity: {2}";
	}
}
