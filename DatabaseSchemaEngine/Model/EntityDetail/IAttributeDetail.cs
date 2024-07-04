using DatabaseSchemaEngine.Validator;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	using DatabaseSchemaEngine.Formatter;
	public interface IAttributeDetail : IValidatable, IFormattable
	{
		string Name { get; set; }
		string Type { get; set; }
	}
}