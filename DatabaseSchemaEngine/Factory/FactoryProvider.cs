using DatabaseSchemaEngine.Enum;

namespace DatabaseSchemaEngine.Factory
{
	/// <summary>
	/// Abstaract factory, provides the factory for target framework.
	/// </summary>
	public class FactoryProvider
	{
		public static ISchemaGeneratorFactory GetSchemaGeneratorFactory(TargetDatabaseFrameworkValues targetFramework)
		{
			if (targetFramework.Equals(TargetDatabaseFrameworkValues.SFCDB)) 
			{
				return new SFCDBSchemaGeneratorFactory();
			}

			return null;
		}
	}
}
