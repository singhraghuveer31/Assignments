using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Helper.FileHelper;
using DatabaseSchemaEngine.Model.EntityDetail;
using Newtonsoft.Json;

namespace DatabaseSchemaEngine.Model.DomainModelGenerator
{
	public class DomainModelMetadataGenerator : IDomainModelMetadataGenerator
	{
		public IModelGenerationOutput Output { get; protected set; }

		public DomainModelMetadataGenerator() 
		{
			Output = new ModelGenerationOutput();
		}

		public void GenerateDomainModel(IEnumerable<IEntityDetail> entityDetails)
		{
			Output.Content = JsonConvert.SerializeObject(entityDetails, Formatting.Indented);
			FileWriterHelper.WriteFile(GetFileName, Output.Content);
		}

		private string GetFileName() 
		{
			try
			{
				var path = Path.Combine(Common.DomainModelMetadataOutputPath, Common.DomainModelMetadataFileName);
				Output.OutputFilePath = path;
				return path;
			}
			catch (Exception ex) 
			{
				throw new Exception("Failed to get file name for model generation.", ex);
			}
		}
	}
}
