using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider
{
	public interface IValidatorProvider
	{
		List<string> Validate(List<IEntityDetail> entityDetails);
	}
}
