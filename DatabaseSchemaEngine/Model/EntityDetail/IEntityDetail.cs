
using DatabaseSchemaEngine.Validator;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	using DatabaseSchemaEngine.Formatter;
	public interface IEntityDetail : IValidatable, IFormattable
	{
		int Id { get; set; }
		List<AttributeDetail> Attributes { get; set; }
		string EntityName { get; set; }
	}
}