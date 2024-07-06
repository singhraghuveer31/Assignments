using DatabaseSchemaEngine.Constants;

namespace DatabaseSchemaEngine.Validator.ValidationMessage
{
	public class SFCDBValidationMessageProvider : ValidationMessageProviderBase
	{
		public SFCDBValidationMessageProvider() : base("data store", "prop")
		{
		}

		#region Overridden methods

		protected override string GetAllowedSpecialCharacters()
		{
			return string.Join(",", ValidationConstant.SFCDBAllowedSpecialChars);
		}

		protected override int GetColumnMaxLength()
		{
			return ValidationConstant.SFCDBPropMaxLength;
		}

		protected override int GetColumnMinLength()
		{
			return ValidationConstant.SFCDBPropMinLength;
		}

		protected override int GetTableLength()
		{
			return ValidationConstant.SFCDBDataStoreLength;
		}

		#endregion
	}
}
