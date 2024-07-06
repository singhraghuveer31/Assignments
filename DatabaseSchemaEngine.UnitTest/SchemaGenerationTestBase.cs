namespace DatabaseSchemaEngine.UnitTest
{
    public class SchemaGenerationTestBase
    {
        protected void SetupSFCDBConfig(string outputDirPath)
        {
            System.Configuration.ConfigurationManager.AppSettings["schema:sfcdb:database-schema:Directory.outputPath"] = outputDirPath;
            System.Configuration.ConfigurationManager.AppSettings["schema:sfcdb:database-schema-template:Directory.path"] = "C:\\SchemaGenerationTemplates\\SFCDB\\";
        }

        protected void CreateSFCDBOutputDirectory(string outPutDirectoryPath) 
        {
            Directory.CreateDirectory(outPutDirectoryPath);
        }

        protected void EmptySFCDBOutputDirectory(string outPutDirectoryPath)
        {
            var files = Directory.GetFiles(outPutDirectoryPath);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
