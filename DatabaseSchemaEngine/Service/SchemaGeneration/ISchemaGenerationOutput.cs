
namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
	/// <summary>
	/// Represents schema generation output. 
	/// </summary>
	public interface ISchemaGenerationOutput
	{
		/// <summary>
		/// Validation messages.
		/// </summary>
		List<string> ValidationMessages { get; set; }

		/// <summary>
		/// Indicates whether schema generation is successful or not.
		/// </summary>
		bool IsSuccess { get; set; }
	}
}