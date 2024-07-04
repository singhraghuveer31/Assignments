
using DatabaseSchemaEngine.Validator;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	public interface IEntityDetail : IValidatable
	{
		List<IAttributeDetail> Attributes { get; set; }
		string EnityName { get; set; }
	}
}