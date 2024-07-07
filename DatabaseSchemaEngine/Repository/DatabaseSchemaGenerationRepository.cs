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

			var schemaMappingDetails = new SchemaMappingDetail(GetSFCDBMapping(), SFCDBSchemaGeneratorConstant.DataStoreTemplateFileName, SFCDBSchemaGeneratorConstant.PropTemplateFileName, "mdl", "cs");
			results.Add(Enum.TargetDatabaseFrameworkValues.SFCDB.ToString(), schemaMappingDetails);

			schemaMappingDetails = new SchemaMappingDetail(GetSQLiteMapping(), SQLiteSchemaGeneratorConstant.TableTemplateFileName,SQLiteSchemaGeneratorConstant.ColumnTemplateFileName, "sql", "cs");
			results.Add(Enum.TargetDatabaseFrameworkValues.SQLite.ToString(), schemaMappingDetails);

			return results;
		}

		private static Map<string, string> GetSFCDBMapping()
		{
			var sfcdbTypeMap = new Map<string, string>();
			sfcdbTypeMap.Add("String", "unlimited_text");
			sfcdbTypeMap.Add("Int16", "small_numerical");
			sfcdbTypeMap.Add("Int", "numerical");

			return sfcdbTypeMap;
		}

		private static Map<string, string> GetSQLiteMapping()
		{
			var sqliteTypeMap = new Map<string, string>();
			sqliteTypeMap.Add("String", "text");
			sqliteTypeMap.Add("Int16", "num");
			sqliteTypeMap.Add("Int", "int");

			return sqliteTypeMap;
		}
	}
}
