namespace DatabaseSchemaEngine.Constants
{
	public static class ValidationConstant
	{
		public const int SFCDBDataStoreLength = 8;
		public const int SFCDBPropMinLength = 4;
		public const int SFCDBPropMaxLength = 16;
		public static List<char> SFCDBAllowedSpecialChars = new() { '_', '-' };
	}
}
