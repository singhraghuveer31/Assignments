using DatabaseSchemaEngine.Enum;
using System.Text.Json;

namespace DatabaseSchemaEngine.Formatter.SyntaxFormatRules
{
	public class NamingConventionRule : IFormatRule
	{
		private readonly NamingConventionValues namingConventionRule;

		public NamingConventionRule(NamingConventionValues namingConventionRule)
		{
			this.namingConventionRule = namingConventionRule;
		}

		public string Format(string syntax)
		{
			if (namingConventionRule == NamingConventionValues.UpperCase)
			{
				syntax = syntax.ToUpperInvariant();
			}
			else if (namingConventionRule == NamingConventionValues.LowerCase)
			{
				syntax = syntax.ToLowerInvariant();
			}
			else if (namingConventionRule == NamingConventionValues.CamelCase)
			{
				var words = syntax.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
				words = words
					.Select(word => char.ToUpper(word[0]) + word.Substring(1))
					.ToArray();

				syntax = string.Join(string.Empty, words);
				syntax = char.ToLowerInvariant(syntax[0]) + syntax.Substring(1);
			}
			else if (namingConventionRule == NamingConventionValues.CapitalCaseFirst)
			{
				syntax = char.ToUpper(syntax[0]) + syntax.Substring(1).ToLowerInvariant();
			}

			return syntax;
		}
	}
}
