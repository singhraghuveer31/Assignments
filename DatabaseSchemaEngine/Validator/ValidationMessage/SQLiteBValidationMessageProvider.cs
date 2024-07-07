namespace DatabaseSchemaEngine.Validator.ValidationMessage
{
	public class SQLiteValidationMessageProvider : ValidationMessageProviderBase
	{
		public SQLiteValidationMessageProvider() : base("table", "column")
		{
		}

		protected override string GetAllowedSpecialCharacters()
		{
			throw new NotImplementedException();
		}

		protected override int GetColumnMaxLength()
		{
			throw new NotImplementedException();
		}

		protected override int GetColumnMinLength()
		{
			throw new NotImplementedException();
		}

		protected override int GetTableLength()
		{
			throw new NotImplementedException();
		}
	}
}
