using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider
{
	/// <summary>
	/// Validator provider
	/// </summary>
	public interface IValidatorProvider
	{
		/// <summary>
		/// Validates the <paramref name="entityDetails"/>
		/// </summary>
		/// <param name="entityDetails">Entity details</param>
		/// <returns>List of error messages if there were any rule validation failures.</returns>
		List<string> Validate(List<IEntityDetail> entityDetails);
	}
}
