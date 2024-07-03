
namespace DatabaseSchemaEngine.Model.EntityDetail
{
	public interface IEntityDetail
	{
		List<AttributeDetail> Attributes { get; set; }
		string EnityName { get; set; }
	}
}