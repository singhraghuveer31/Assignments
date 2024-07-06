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
			Output = new SchemaGenerationOutput(new List<string>(), false);
		}

		public void GenerateDatabaseSchema(ISchemaGenerationInput schemaGenerationInput)
		{
			try
			{
				var factory = FactoryProvider.GetSchemaGeneratorFactory(schemaGenerationInput.TargetFramework);

				//Validate.
				if (!Validate(factory, schemaGenerationInput.EntityDetails))
				{
					return;
				}

				//Store Metadata.
				GenerateDomainModelMetaData(factory, schemaGenerationInput.EntityDetails);

				//Format.
				var formmatedData = Format(factory, schemaGenerationInput);

				//Generate.
				var generator = factory.GetDatabaseSchemaGenerator();
				generator?.GenerateDatabaseSchema(formmatedData);
				
				Output.IsSuccess = true;
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Generate database schema failed", nameof(GenerateDatabaseSchema));
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
			finally 
			{
				Validator.Validator.ClearValidationRules();
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
			finally 
			{
				Formatter.Formatter.ClearFormatRules();
			}
			return entities;
		}

		private void GenerateDomainModelMetaData(ISchemaGeneratorFactory schemaGeneratorFactory, IEnumerable<IEntityDetail> entityDetails) 
		{
			try 
			{
				var domainModelGenerator = schemaGeneratorFactory.GetDomainModelGenerator();
				domainModelGenerator.GenerateDomainModel(entityDetails);
			}
			catch (Exception ex) 
			{
				throw new Exception("Error occurred while generating domain model.", ex);
			}
		}
	}
}
