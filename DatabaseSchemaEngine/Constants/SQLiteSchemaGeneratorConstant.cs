namespace DatabaseSchemaEngine.Constants
{
	/// <summary>
	/// Defines constants required for schema generator.
	/// </summary>
	public class SQLiteSchemaGeneratorConstant
	{
		/// <summary>
		/// Defile the template file path for table of SQLite database schema generation.
		/// </summary>
		public const string TableTemplateFileName = "SQLiteTableTemplate.df";

		/// <summary>
		/// Defile the template file path for columns of SQLite database schema generation.
		/// </summary>
		public const string ColumnTemplateFileName = "SQLiteColumnTemplate.df";

		/// <summary>
		/// Define the tag for data store name in SQLite template file.
		/// </summary>
		public const string TableNameTag = "<table name>";

		/// <summary>
		/// Defines the placeholder for column data store in SQLite template file.
		/// </summary>
		public const string TablePropertyTag = "<column>";

		/// <summary>
		/// Defines the tag for property name in SQLite template file.
		/// </summary>
		public const string ColumnNameTag = "<column name>";

		/// <summary>
		/// Defines the tag for type specification in SQLite template file.
		/// </summary>
		public const string DataTypeNameTag = "<type specification>";

		/// <summary>
		/// Defines the separator for multi definition in SQLite template file.
		/// </summary>
		public const string MultiDefSeparator = "/* ---End of table creation---*/";
	}
}
