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

		public void GenerateDatabaseSchema(string targetFramework, IEnumerable<IEntityDetail> entityDetails)
		{
			try
			{
				var factory = FactoryProvider.GetSchemaGeneratorFactory(targetFramework, logger);

				//Validate.
				if (!Validate(factory, entityDetails))
				{
					return;
				}

				//Format.
				var formmatedData = Format(factory, entityDetails);

				//Generate.
				var generator = factory.GetDatabaseSchemaGenerator();
				generator?.GenerateDatabaseSchema(formmatedData);

				//Store Metadata.
				GenerateDomainModelMetaData(factory, entityDetails);
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

		private List<IEntityDetail> Format(ISchemaGeneratorFactory schemaGeneratorFactory, IEnumerable<IEntityDetail> entityDetails)
		{
			var entities = entityDetails.ToList();
			try
			{
				var formatterProvider = schemaGeneratorFactory.GetFormatterProvider();
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
