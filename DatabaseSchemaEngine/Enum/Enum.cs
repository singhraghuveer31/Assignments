namespace DatabaseSchemaEngine.Enum
{
	/// <summary>
	/// Defines the available target database frameworks.
	/// </summary>
	public enum TargetDatabaseFrameworkValues
	{
		/// <summary>
		/// Specifies SFCDB database framework
		/// </summary>
		SFCDB,

		/// <summary>
		/// Specifies SQLite database framework
		/// </summary>
		SQLite
	}

	public enum NamingConventionValues
	{
		UpperCase = 0,
		CapitalCaseFirst,
		LowerCase,
		CamelCase
	}

	public enum PrefixConventionValues
	{
		Letter=0,
		UnderScore
	}

	public enum TypeNameValues 
	{
		Entity,
		Attibute
	}

	public enum SchemaGenerationOptionValues
	{
		ReplaceSpaceWithUnderScore,
		AnotherOption
	}
}
