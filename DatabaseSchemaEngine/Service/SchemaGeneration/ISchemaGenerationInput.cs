using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	/// <summary>
	/// Represents schema generation input.
	/// </summary>
	public interface ISchemaGenerationInput
	{
		/// <summary>
		/// Represents target database framework for schema generation.
		/// </summary>
		TargetDatabaseFrameworkValues TargetFramework { get; set; }

		/// <summary>
		/// Entity details required for schema generation.
		/// </summary>
		IEnumerable<IEntityDetail> EntityDetails { get; set; }

		/// <summary>
		/// Contains the schema formatting options.
		/// </summary>
		IEnumerable<ILookup> SchemaGenerationOptions { get; set; }
	}
}