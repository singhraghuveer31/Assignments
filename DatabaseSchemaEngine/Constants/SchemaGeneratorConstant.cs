namespace DatabaseSchemaEngine.Constants
{
	/// <summary>
	/// Defines constants required for schema generator.
	/// </summary>
	public class SchemaGeneratorConstant
	{
		/// <summary>
		/// Defines templates folder path.
		/// </summary>
		public const string TemplateFolderPath = "C:\\SchemaGenerationTemplates\\";

		/// <summary>
		/// Defile the template file path for data store of SFCDB database schema generation.
		/// </summary>
		public const string SFCDBDataStoreTemplateFileName = "SFCDB\\SFCDBDataStoreTemplate.df";

		/// <summary>
		/// Defile the template file path for properties of SFCDB database schema generation.
		/// </summary>
		public const string SFCDBPropTemplateFileName = "SFCDB\\SFCDBPropTemplate.df";

		/// <summary>
		/// Define the tag for data store name in SFCDB template file.
		/// </summary>
		public const string SFCDBDataStoreNameTag = "<data store name>";

		/// <summary>
		/// Defines the placeholder for properties data store in SFCDB template file.
		/// </summary>
		public const string SFCDBDataStorePropertyTag = "<prop>";

		/// <summary>
		/// Defines the tag for property name in SFCDB template file.
		/// </summary>
		public const string SFCDBDataPropertyNameTag = "<property name>";

		/// <summary>
		/// Defines the tag for type specification in SFCDB template file.
		/// </summary>
		public const string SFCDBDataTypeNameTag = "<type specification>";

		/// <summary>
		/// Defines the separator for multi definition in SFCDB template file.
		/// </summary>
		public const string SFCDBMultiDefSeparator = "// ---";

		/// <summary>
		/// Defines the separator for multi definition in SFCDB template file.
		/// </summary>
		public const string SFCDBSchemaOutputPath = "C:\\SchemaGenerationOutput\\";
	}
}
