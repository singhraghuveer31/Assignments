using DatabaseSchemaEngine.Enum;
using Serilog;

namespace DatabaseSchemaEngine.Factory
{
	public class FactoryProvider
	{
		public static ISchemaGeneratorFactory GetSchemaGeneratorFactory(TargetDatabaseFrameworkValues targetFramework, ILogger logger)
		{
			if (targetFramework.Equals(TargetDatabaseFrameworkValues.SFCDB)) 
			{
				return new SFCDBSchemaGeneratorFactory(logger);
			}

			return null;
		}
	}
}
