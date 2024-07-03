using DatabaseSchemaEngine.Model.EntityDetail;
using Serilog;
using Newtonsoft.Json;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Helper;

namespace DatabaseSchemaEngine.Model.SchemaFileParser
{
	internal class SFCDBSchemaFileParser : ISchemaFileParser
	{
		private readonly ILogger logger;
		private readonly ISchemaMapper schemaMapper;
		private Map<string, string>? schemaMappings;

		public SFCDBSchemaFileParser(ILogger logger, ISchemaMapper schemaMapper)
		{
			this.logger = logger;
			this.schemaMapper = schemaMapper;
			Initialize();
		}

		private void Initialize() 
		{
			var mappingDetail = schemaMapper.GetSchemaMappings();

			if (mappingDetail != null) 
			{
				schemaMappings = mappingDetail.TypeMappings;
			}
		}

		public string ParseSchemaFile()
		{
			if (schemaMappings == null || schemaMappings.Reverse == null) 
			{
				return string.Empty;
			}

			var output = string.Empty;
			try 
			{
				var file = GetOutputSchemaFile();
				List<IEntityDetail> entityDetails = ParseSchemaFile(file);
				output = JsonConvert.SerializeObject(entityDetails); 
			}
			catch (Exception e) 
			{
				logger.Error(e, "Error parsing the file");
			}

			return output;
		}

		private string GetOutputSchemaFile()
		{
			var outputFolder = Constants.SchemaGeneratorConstant.SFCDBSchemaOutputPath;

			var files = Directory.GetFiles(outputFolder);

			if (!files.Any())
			{
				throw new Exception("No schema files found.");
			}

			return files[0];

		}

		private List<IEntityDetail> ParseSchemaFile(string file)
		{
			var dataStores = file.Split(Constants.SchemaGeneratorConstant.SFCDBMultiDefSeparator);

			var entityDetails = new List<IEntityDetail>();

			foreach (var dataStore in dataStores)
			{
				var entity = CreateEntity(dataStore);
				if (entity != null)
				{
					entityDetails.Add(entity);
				}
			}

			return entityDetails;
		}

		private IEntityDetail? CreateEntity(string dataStore)
		{
			IEntityDetail? entity = null;
			try
			{
				var dataStoreDef = GetDataStoreDefintion(dataStore);

				if (string.IsNullOrEmpty(dataStoreDef)) 
				{
					return null;
				}

				var entityName = GetEntityName(dataStoreDef);
				if (string.IsNullOrEmpty(entityName))
				{
					return null;
				}

				var properties = dataStoreDef.Split(Constants.SchemaParserConstant.SFCDBPropStatement);
				var attributes = new List<AttributeDetail>();

				foreach (var property in properties)
				{
					var attribute = GetAttribute(property);
					if (attribute != null)
					{
						attributes.Add(attribute);
					}
				}

				entity = new EntityDetail.EntityDetail(entityName, attributes);
			}
			catch(Exception ex) 
			{
				logger.Error(ex, $"Failed to create entity for statement {dataStore}");
			}

			return entity;
		}

		private string GetEntityName(string dataStoreDef) 
		{
			string entityName = string.Empty;
			try
			{
				var values = dataStoreDef.Split(Environment.NewLine);
				entityName = values[0].Trim();
			}
			catch (Exception ex) 
			{
				logger.Error(ex, $"Failed to get entity name from statement: {dataStoreDef}");
			}
			return entityName;
		}

		private string GetDataStoreDefintion(string dataStore) 
		{
			var dataStoreDef = string.Empty;
			try
			{
				var startIndex = dataStore.IndexOf(Constants.SchemaParserConstant.SFCDBDataStoreStartStatement) + Constants.SchemaParserConstant.SFCDBDataStoreStartStatement.Length;
				var lastIndex = dataStore.IndexOf(Constants.SchemaParserConstant.SFCDBDataStoreEndStatement);
				dataStoreDef = dataStore.Substring(startIndex, lastIndex - startIndex);
			}
			catch (Exception ex) 
			{
				logger.Error(ex, "Error parsing data store");
			}

			return dataStoreDef;
		}

		private AttributeDetail? GetAttribute(string property)
		{
			AttributeDetail? attribute = null;
			try
			{
				var propNamTypePair = property.Split(Environment.NewLine);
				var name = propNamTypePair[0];
				var type = GetTypeMapping(propNamTypePair[1]);

				attribute = new AttributeDetail(name, type);
			}
			catch (Exception ex) 
			{
				logger.Error(ex, $"Failed while getting the property details for property {property}");
			}

			return attribute;
		}

		private string GetTypeMapping(string schemaTypeName) 
		{
			if (schemaMappings != null && schemaMappings.Reverse.ContainsKey(schemaTypeName))
			{
				return schemaMappings.Reverse[schemaTypeName];
			}
			else 
			{
				throw new Exception($"Domain model mapping not available for type: {schemaTypeName} ");
			}
		}
	}
}
