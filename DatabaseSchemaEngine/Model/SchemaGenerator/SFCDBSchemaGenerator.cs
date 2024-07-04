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
				logger.Error(ex, "Failed to store file.");
			}
		}

		private string GetFileName(string fileExtension)
		{
			var fileName = string.Empty;
			try
			{
				fileName = $"{DateTime.Now.ToString("yyy-dd-MM")}";

				var outputDirectory = Constants.SFCDBSchemaGeneratorConstant.SchemaOutputPath;

				fileName = FileNameHelper.GetNextFileName(fileName, outputDirectory, fileExtension);

				fileName = $"{Path.Combine(Constants.SFCDBSchemaGeneratorConstant.SchemaOutputPath, fileName)}.{fileExtension}";
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Error while getting the file name.");
			}

			return fileName;
		}
	}
}
