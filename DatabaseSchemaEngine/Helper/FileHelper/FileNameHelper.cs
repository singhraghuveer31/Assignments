namespace DatabaseSchemaEngine.Helper.FileHelper
{
	public static class FileNameHelper
	{
		public static string GetNextFileName(string fileName, string directory, string fileExtension) 
		{
			var filePaths = Directory.GetFiles(directory, $"*.{fileExtension}");

			if (filePaths.Any())
			{
				var latestFileName = filePaths.OrderByDescending(s => s, StringComparer.Ordinal).First();

				latestFileName = Path.GetFileName(latestFileName);
				if ($"{fileName}.{fileExtension}" == latestFileName)
				{
					fileName = $"{fileName}_{1}";
				}
				else
				{
					var sequenceNoIndex = latestFileName.LastIndexOf("_");

					if (sequenceNoIndex > fileName.Length - 1 && fileName == latestFileName.Substring(0, sequenceNoIndex))
					{
						var intLength = latestFileName.LastIndexOf(".") - sequenceNoIndex - 1;
						var nextSeqNo = Convert.ToInt32(latestFileName.Substring(sequenceNoIndex + 1, intLength)) + 1;
						fileName = $"{fileName}_{nextSeqNo}";
					}
				}
			}

			return fileName;
		}
	}
}
