using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Helper;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
namespace DatabaseSchemaEngine.Repository
{
	public class DatabaseSchemaGenerationRepository : IDatabaseSchemaGenerationRepository
	{
		public Dictionary<string, SchemaMappingDetail> GetSchemaMappings()
		{
			//Assuming configuration will be saved in the database table.
			//Here we are returning hard coded values.
			var results = new Dictionary<string, SchemaMappingDetail>();

			var sfcdbTypeMap = new Map<string, string>();
			sfcdbTypeMap.Add("String", "unlimited_text");
			sfcdbTypeMap.Add("Int16", "small_numerical");
			sfcdbTypeMap.Add("Int", "numerical");

			var schemaMappingDetails = new SchemaMappingDetail(sfcdbTypeMap, SchemaGeneratorConstant.SFCDBDataStoreTemplateFileName, SchemaGeneratorConstant.SFCDBPropTemplateFileName, "mdl", "cs");
			results.Add(Enum.TragetDatabaseFrameworkValues.SFCDB.ToString(), schemaMappingDetails);

			return results;
		}
	}
}
