using Serilog;

namespace DatabaseSchemaEngine.Factory
{
	public class FactoryProvider
	{
		public static ISchemaGeneratorFactory GetSchemaGeneratorFactory(string targetFramework, ILogger logger)
		{
			if (targetFramework.Equals(Enum.TragetDatabaseFrameworkValues.SFCDB.ToString())) 
			{
				return new SFCDBSchemaGeneratorFactory(logger);
			}

			return null;
		}
	}
}
