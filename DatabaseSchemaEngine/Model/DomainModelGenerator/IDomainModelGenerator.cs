using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Model.DomainModelGenerator
{
	public interface IDomainModelGenerator
	{
		IModelGenerationOutput Output { get; }
		void GenerateDomainModel(IEnumerable<IEntityDetail> entityDetails);
	}
}
