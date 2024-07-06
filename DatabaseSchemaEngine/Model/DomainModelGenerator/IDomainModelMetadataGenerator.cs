using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Model.DomainModelGenerator
{
	public interface IDomainModelMetadataGenerator
	{
		IModelGenerationOutput Output { get; }
		void GenerateDomainModel(IEnumerable<IEntityDetail> entityDetails);
	}
}
