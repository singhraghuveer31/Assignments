namespace DatabaseSchemaEngine.Model.SchemaGenerator
{
	using DatabaseSchemaEngine.Helper;
	using DatabaseSchemaEngine.Helper.FileHelper;
	using DatabaseSchemaEngine.Model.SchemaMapper;

	/// <summary>
	/// Implementation for SFCDB schema generation.
	/// </summary>
	public class SFCDBSchemaGenerator : SchemaGeneratorBase
	{
		public SFCDBSchemaGenerator(ISchemaMapper schemaMapper) : base(schemaMapper)
		{ 
		}

		protected override string GetTableNameTag()
		{
			return Constants.SFCDBSchemaGeneratorConstant.DataStoreNameTag;
		}

		protected override string GetPropertyPlaceHolder()
		{
			return Constants.SFCDBSchemaGeneratorConstant.DataStorePropertyTag;
		}

		protected override string GetPropertyTypePlaceHolder()
		{
			return Constants.SFCDBSchemaGeneratorConstant.DataTypeNameTag;
		}

		protected override string GetPropertyNamePlaceHolder()
		{
			return Constants.SFCDBSchemaGeneratorConstant.DataPropertyNameTag;
		}

		protected override string GetMultiSchemaDefinitionSeparator()
		{
			return Constants.SFCDBSchemaGeneratorConstant.MultiDefSeparator;
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
			var fileName = string.Empty;
			try
			{
				fileName = $"{DateTime.Now.ToString("yyy-dd-MM")}";

				var outputDirectory = Helper.Configuration.SFCDDatabaseSchemaOuputDirectory;

				if (outputDirectory == null) 
				{
					throw new Exception("No configuration found for SFCDB Database schema output directory.");
				}

				fileName = FileNameHelper.GetNextFileName(fileName, outputDirectory, fileExtension);

				fileName = $"{Path.Combine(outputDirectory, fileName)}.{fileExtension}";
			}
			catch (Exception ex)
			{
				throw new Exception("Error while getting the file name.", ex);
			}

			return fileName;
		}

		protected override string GetSchemaTemplateDirectoryPath(string templateFileName)
		{
			return Path.Combine(Configuration.SFCDDatabaseSchemaTemplateDirectory, templateFileName);
		}
	}
}
