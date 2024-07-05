
using DatabaseSchemaEngine.Validator;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	using DatabaseSchemaEngine.Formatter;
	public interface IEntityDetail : IValidatable, IFormattable
	{
		List<IAttributeDetail> Attributes { get; set; }
		string EnityName { get; set; }
	}
}