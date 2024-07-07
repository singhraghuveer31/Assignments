namespace DatabaseSchemaEngine.Model.SchemaGenerator
{
	using DatabaseSchemaEngine.Constants;
	using DatabaseSchemaEngine.Helper;
	using DatabaseSchemaEngine.Helper.FileHelper;
	using DatabaseSchemaEngine.Model.SchemaMapper;

	/// <summary>
	/// Implementation for  schema generation.
	/// </summary>
	public class SQLiteSchemaGenerator : SchemaGeneratorBase
	{
		public SQLiteSchemaGenerator(ISchemaMapper schemaMapper) : base(schemaMapper)
		{ 
		}

		protected override string GetTableNameTag()
		{
			return Constants.SQLiteSchemaGeneratorConstant.TableNameTag;
		}

		protected override string GetPropertyPlaceHolder()
		{
			return Constants.SQLiteSchemaGeneratorConstant.TablePropertyTag;
		}

		protected override string GetPropertyTypePlaceHolder()
		{
			return Constants.SQLiteSchemaGeneratorConstant.DataTypeNameTag;
		}

		protected override string GetPropertyNamePlaceHolder()
		{
			return Constants.SQLiteSchemaGeneratorConstant.ColumnNameTag;
		}

		protected override string GetMultiSchemaDefinitionSeparator()
		{
			return Constants.SQLiteSchemaGeneratorConstant.MultiDefSeparator;
		}

		protected override void WriteFile(string content, string fileExtension)
		{
			try
			{
				FileWriterHelper.WriteFile(()=> GetFileName(fileExtension), content);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to store file.", ex);
			}
		}

		private string GetFileName(string fileExtension)
		{
			return Configuration.SQLiteDatabaseSchemaOuputDirectory + "SQLiteSchemaFile.sql";
		}

		protected override string GetSchemaTemplateDirectoryPath(string templateFileName)
		{
			return Path.Combine(Configuration.SQLiteDatabaseSchemaTemplateDirectory, templateFileName);
		}

		protected override string GetAttributeSeparator()
		{
			return "," + Environment.NewLine;
		}
	}
}
