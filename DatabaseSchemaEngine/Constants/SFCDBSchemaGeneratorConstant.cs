namespace DatabaseSchemaEngine.Constants
{
	/// <summary>
	/// Defines constants required for schema generator.
	/// </summary>
	public class SFCDBSchemaGeneratorConstant
	{
		/// <summary>
		/// Defile the template file path for data store of SFCDB database schema generation.
		/// </summary>
		public const string DataStoreTemplateFileName = "SFCDBDataStoreTemplate.df";

		/// <summary>
		/// Defile the template file path for properties of SFCDB database schema generation.
		/// </summary>
		public const string PropTemplateFileName = "SFCDBPropTemplate.df";

		/// <summary>
		/// Define the tag for data store name in SFCDB template file.
		/// </summary>
		public const string DataStoreNameTag = "<data store name>";

		/// <summary>
		/// Defines the placeholder for properties data store in SFCDB template file.
		/// </summary>
		public const string DataStorePropertyTag = "<prop>";

		/// <summary>
		/// Defines the tag for property name in SFCDB template file.
		/// </summary>
		public const string DataPropertyNameTag = "<property name>";

		/// <summary>
		/// Defines the tag for type specification in SFCDB template file.
		/// </summary>
		public const string DataTypeNameTag = "<type specification>";

		/// <summary>
		/// Defines the separator for multi definition in SFCDB template file.
		/// </summary>
		public const string MultiDefSeparator = "// ---";

		/// <summary>
		/// Defines the output path for SFCDB schema file.
		/// </summary>
		public const string SchemaOutputPath = "C:\\SchemaGenerationOutput\\";
	}
}
