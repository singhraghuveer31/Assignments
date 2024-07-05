using DatabaseSchemaEngine.Factory;
using DatabaseSchemaEngine.Model.EntityDetail;
using Serilog;

namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	/// <summary>
	/// Implements methods required for database schema generation.
	/// </summary>
	public class DatabaseSchemaGenerationService : IDatabaseSchemaGeneratorService
	{
		private readonly ILogger logger;

		public ISchemaGenerationOutput Output { get; set; }

		public DatabaseSchemaGenerationService(ILogger logger)
		{
			this.logger = logger;
			Output = new SchemaGenerationOutput(string.Empty, string.Empty, new List<string>());
		}

		public void GenerateDatabaseSchema(ISchemaGenerationInput schemaGenerationInput)
		{
			try
			{
				var factory = FactoryProvider.GetSchemaGeneratorFactory(schemaGenerationInput.TargetFramework, logger);

				//Validate.
				if (!Validate(factory, schemaGenerationInput.EntityDetails))
				{
					return;
				}

				//Format.
				var formmatedData = Format(factory, schemaGenerationInput);

				//Generate.
				var generator = factory.GetDatabaseSchemaGenerator();
				generator?.GenerateDatabaseSchema(formmatedData);

				//Store Metadata.
				GenerateDomainModelMetaData(factory, schemaGenerationInput.EntityDetails);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Generate database schema failed");
			}
		}

		private bool Validate(ISchemaGeneratorFactory schemaGeneratorFactory, IEnumerable<IEntityDetail> entityDetails)
		{
			try
			{
				if (entityDetails == null || !entityDetails.Any()) 
				{
					throw new ArgumentNullException(nameof(entityDetails));
				}

				var validator = schemaGeneratorFactory.GetValidatorProvider();
				var errorMessages = validator.Validate(entityDetails.ToList());
				if (errorMessages != null && errorMessages.Any())
				{
					Output.ValidationMessages = errorMessages;
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error while validating the data.", ex);
			}

			return true;
		}

		private List<IEntityDetail> Format(ISchemaGeneratorFactory schemaGeneratorFactory, ISchemaGenerationInput schemaGenerationInput)
		{
			var entities = schemaGenerationInput.EntityDetails.ToList();
			try
			{
				var formatterProvider = schemaGeneratorFactory.GetFormatterProvider(schemaGenerationInput.SchemaGenerationOptions);
				formatterProvider.Format(entities);
			}
			catch (Exception ex)
			{
				throw new Exception("Error while formatting the data.", ex);
			}
			return entities;
		}

		private void GenerateDomainModelMetaData(ISchemaGeneratorFactory schemaGeneratorFactory, IEnumerable<IEntityDetail> entityDetails) 
		{
			try 
			{
				var domainModelGenerator = schemaGeneratorFactory.GetDomainModelGenerator();
				domainModelGenerator.GenerateDomainModel(entityDetails);
				Output.DomainModelMetaData = domainModelGenerator.Output.Content;
			}
			catch (Exception ex) 
			{
				throw new Exception("Error occurred while generating domain model.", ex);
			}
		}
	}
}
