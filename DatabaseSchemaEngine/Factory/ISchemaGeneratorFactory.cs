using DatabaseSchemaEngine.Model.SchemaGenerator;
using DatabaseSchemaEngine.Model.SchemaMapper;

namespace DatabaseSchemaEngine.Factory
{
	public interface ISchemaGeneratorFactory
	{
		IDatabaseSchemaGenerator GetDatabaseSchemaGenerator();
		ISchemaMapper GetSchemaMapper();
	}
}