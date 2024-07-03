namespace DatabaseSchemaEngine.Model.SchemaGenerator
{
	using DatabaseSchemaEngine.Helper.FileHelper;
	using DatabaseSchemaEngine.Model.SchemaMapper;
	using DatabaseSchemaEngine.Repository;
	using Serilog;

	/// <summary>
	/// Implementation for SFCDB schema generation.
	/// </summary>
	public class SFCDBSchemaGenerator : SchemaGeneratorBase
	{
		private readonly ILogger logger;

		public SFCDBSchemaGenerator(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository, ILogger logger, ISchemaMapper schemaMapper) : base(databaseSchemaGenerationRepository, logger, schemaMapper)
		{
			this.logger = logger;
		}

		protected override string GetTableNameTag()
		{
			return Constants.SchemaGeneratorConstant.SFCDBDataStoreNameTag;
		}

		protected override string GetPropertyPlaceHolder()
		{
			return Constants.SchemaGeneratorConstant.SFCDBDataStorePropertyTag;
		}

		protected override string GetPropertyTypePlaceHolder()
		{
			return Constants.SchemaGeneratorConstant.SFCDBDataTypeNameTag;
		}

		protected override string GetPropertyNamePlaceHolder()
		{
			return Constants.SchemaGeneratorConstant.SFCDBDataPropertyNameTag;
		}

		protected override string GetMultiSchemaDefinitionSeparator()
		{
			return Constants.SchemaGeneratorConstant.SFCDBMultiDefSeparator;
		}

		protected override void WriteFile(string content, string fileExtension)
		{
			try
			{
				FileWriterHelper.WriteFile(()=> GetFileName(fileExtension), content);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Failed to store file.");
			}
		}

		private string GetFileName(string fileExtension)
		{
			var fileName = string.Empty;
			try
			{
				fileName = $"{DateTime.Now.ToString("yyy-dd-MM")}";

				var outputDirectory = Constants.SchemaGeneratorConstant.SFCDBSchemaOutputPath;

				fileName = FileNameHelper.GetNextFileName(fileName, outputDirectory, fileExtension);

				fileName = $"{Path.Combine(Constants.SchemaGeneratorConstant.SFCDBSchemaOutputPath, fileName)}.{fileExtension}";
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Error while getting the file name.");
			}

			return fileName;
		}
	}
}
