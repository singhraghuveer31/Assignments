using DatabaseSchemaEngine.Helper;
using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;
using Serilog;
using System.Text;

namespace DatabaseSchemaEngine.Model.SchemaGenerator
{
	/// <summary>
	/// Base class for schema generation, contains the generic implementation.
	/// </summary>
	public abstract class SchemaGeneratorBase : IDatabaseSchemaGenerator
	{
		protected IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;
		private readonly ILogger logger;
		private readonly ISchemaMapper schemaMapper;

		public SchemaGeneratorBase(IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository, ILogger logger, ISchemaMapper schemaMapper)
		{
			this.databaseSchemaGenerationRepository = databaseSchemaGenerationRepository;
			this.logger = logger;
			this.schemaMapper = schemaMapper;
		}

		private ISchemaMappingDetail? GetSchemaMappings() 
		{
			return schemaMapper.GetSchemaMappings();
		}

		protected abstract string GetPropertyNamePlaceHolder();

		protected abstract string GetPropertyTypePlaceHolder();

		protected abstract string GetPropertyPlaceHolder();

		protected abstract string GetTableNameTag();

		protected abstract void WriteFile(string content, string fileExtension);

		protected abstract string GetMultiSchemaDefinitionSeparator();

		protected virtual void MapTableConstraints(StringBuilder content, ISchemaMappingDetail schemaMappingDetail) 
		{
		}

		protected virtual void MapColumnConstraints(StringBuilder content, ISchemaMappingDetail schemaMappingDetail)
		{

		}

		public void GenerateDatabaseSchema(IEnumerable<IEntityDetail> entityDetails)
		{
			var mappings = GetSchemaMappings();

			if (mappings == null)
			{
				return;
			}

			var content = GenerateSchemaFile(mappings, entityDetails.ToList());

			if (!string.IsNullOrWhiteSpace(content)) 
			{
				WriteFile(content, mappings.SchemaFileExtension);
			}
		}

		private string GenerateSchemaFile(ISchemaMappingDetail schemaMappingDetail, List<IEntityDetail> entityDetails)
		{
			StringBuilder content = new StringBuilder();

			foreach (var entity in entityDetails)
			{
				MapTableName(content, schemaMappingDetail, entity.EnityName);
				MapColumns(content, schemaMappingDetail, entity);
				AppendEntitySeparator(content);
			}

			return content.ToString();
		}

		private string GetTemplate(string templateFileName)
		{
			var templateContent = string.Empty;
			try
			{
				var templatePath = Path.Combine(Constants.SFCDBSchemaGeneratorConstant.TemplateFolderPath, templateFileName);
				if (!File.Exists(templatePath))
				{
					throw new Exception($"Invalid configuration for schema mapping file. File not found at path: {templatePath}");
				}

				templateContent = File.ReadAllText(templatePath);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Template file not found.");
			}
			return templateContent;
		}

		private void MapColumns(StringBuilder content, ISchemaMappingDetail schemaMappingDetail, IEntityDetail entity)
		{
			var columnTemplate = GetTemplate(schemaMappingDetail.ColumnTemplateFileName);
			if (columnTemplate == string.Empty)
			{
				return;
			}

			var propList = new List<string>();
			foreach (var attributeDetail in entity.Attributes)
			{
				StringBuilder columnContent = MapColumn(schemaMappingDetail, entity, columnTemplate, attributeDetail);

				MapColumnConstraints(columnContent, schemaMappingDetail);

				propList.Add(columnContent.ToString());
			}

			var proprtyText = string.Empty;
			if (propList.Any())
				proprtyText = string.Join(Environment.NewLine, propList);

			content.Replace(GetPropertyPlaceHolder(), proprtyText);
		}

		private StringBuilder MapColumn(ISchemaMappingDetail schemaMappingDetail, IEntityDetail entity, string columnTemplate, IAttributeDetail attributeDetail)
		{
			StringBuilder columnContent = new StringBuilder().Append(columnTemplate);
			var propType = GetTypeMapping(schemaMappingDetail.TypeMappings, attributeDetail, entity);

			if (propType == string.Empty)
			{
				return new StringBuilder(string.Empty);
			}

			columnContent.Replace(GetPropertyTypePlaceHolder(), propType).Replace(GetPropertyNamePlaceHolder(), attributeDetail.Name);
			return columnContent;
		}

		private void MapTableName(StringBuilder content, ISchemaMappingDetail schemaMappingDetail, string entityName)
		{
			var tableTemplate = GetTemplate(schemaMappingDetail.TableTemplateFileName);
			var tableContent = new StringBuilder(tableTemplate);
			tableContent.Replace(GetTableNameTag(), entityName);
			content.Append(tableContent);

			MapTableConstraints(content, schemaMappingDetail);
		}

		private string GetTypeMapping(Map<string, string> schemaMappings, IAttributeDetail attributeDetail, IEntityDetail entity)
		{
			if (schemaMappings.Forward.ContainsKey(attributeDetail.Type))
			{
				return schemaMappings.Forward[attributeDetail.Type];
			}
			else
			{
				logger.Information($"Mapping not found for type: {attributeDetail.Type}, property: {attributeDetail.Name} in entity: {entity.EnityName}");
				return string.Empty;
			}
		}

		private void AppendEntitySeparator(StringBuilder content)
		{
			content.Append(Environment.NewLine).AppendLine(GetMultiSchemaDefinitionSeparator());
		}
	}
}
