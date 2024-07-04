using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider
{
	public interface IValidatorProvider
	{
		void Register();

		List<string> Validate(List<IEntityDetail> entityDetails);
	}
}
