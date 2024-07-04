using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Formatter.FormatProvider
{
	public interface IFormatterProvider
	{
		void Register();
		void Format(List<IEntityDetail> entityDetails);
	}
}
