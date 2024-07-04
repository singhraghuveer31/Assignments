
using DatabaseSchemaEngine.Enum;

namespace DatabaseSchemaEngine.Validator.SyntaxValidationRule
{
	public class PrefixValidationRule : IValidationRule
	{
		public PrefixValidationRule(PrefixConventionValues prefixConvention)
		{
			PrefixConvention = prefixConvention;
		}

		public PrefixConventionValues PrefixConvention { get; }

		public bool IsValid(string syntax)
		{
			if (PrefixConvention == PrefixConventionValues.Letter)
			{
				return char.IsLetter(syntax[0]);
			}
			else if (PrefixConvention == PrefixConventionValues.UnderScore)
			{
				return syntax[0] == '_';
			}
			else 
			{
				return true;
			}
		}
	}
}
