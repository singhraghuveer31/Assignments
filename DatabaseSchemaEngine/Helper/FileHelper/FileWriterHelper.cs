namespace DatabaseSchemaEngine.Helper.FileHelper
{
	public static class FileWriterHelper
	{
		public static void WriteFile(Func<string> getFilePath, string content)
		{
			var fileName = getFilePath();
			File.WriteAllText(fileName, content);
		}
	}
}
