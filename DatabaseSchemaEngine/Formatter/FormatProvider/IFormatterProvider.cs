using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Formatter.FormatProvider
{
	public interface IFormatterProvider
	{
		void Format(List<IEntityDetail> entityDetails);
	}
}
