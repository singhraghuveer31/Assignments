namespace DatabaseSchemaEngine.Formatter.SyntaxFormatRules
{
	public class ReplaceCharacterRule : IFormatRule
	{
		private readonly char oldChar;
		private readonly char newChar;

		public ReplaceCharacterRule(char oldChar, char newChar) 
		{
			this.oldChar = oldChar;
			this.newChar = newChar;
		}

		public string Format(string syntax)
		{
			return syntax.Replace(oldChar, newChar);
		}
	}
}
