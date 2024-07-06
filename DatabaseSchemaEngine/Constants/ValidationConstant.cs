namespace DatabaseSchemaEngine.Constants
{
	public static class ValidationConstant
	{
		#region Validation Constraints
		
		public const int SFCDBDataStoreLength = 8;
		public const int SFCDBPropMinLength = 4;
		public const int SFCDBPropMaxLength = 16;
		public static List<char> SFCDBAllowedSpecialChars = new() { '_', '-' };

		#endregion

		#region Validation Messages

		public const string TableNameLengthValidation = "Length of {0}: {1} should be {2}";
		public const string TableNameAlreadyExistsValidationMessage = "{0} with name {1} already exists";
		public const string TableNameSpecialCharValidationMessage = "Allowed special characters for {0}: {1} are : {2}";
		public const string TableNamePrefixValidationMessage = "{0}: {1} should start with a {2}";
		public const string ColumnPrefixValidationMessage = "{0}: {1} of {2} {3} should start with a {4}";
		public const string ColumnNameAlreadyExistsValidationMessage = "{0} with name {1} already exists for {2}: {3}.";
		public const string ColumnLengthValidationMessage = "Length of {0}: {1} for {2}:{3} should be between {3} and {4} characters.";
		public const string ColumnSpecialCharValidationMessage = "Allowed special characters for {0}: {1} of {2}: {3} are : {4}";
		#endregion
	}
}
