using DatabaseSchemaEngine.Validator;
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	using DatabaseSchemaEngine.Formatter;
	public class EntityDetail : IEntityDetail
	{
		public EntityDetail(string enityName, List<IAttributeDetail> attributes)
		{
			EnityName = enityName;
			Attributes = attributes;
		}

		public string EnityName { get; set; }

		public List<IAttributeDetail> Attributes { get; set; }

		public bool Validate(IValidationRule rule, IValidationMessageProvider validationMessage, out string errorMessage)
		{
			errorMessage = string.Empty;
			
			if (!rule.IsValid(EnityName))
			{
				errorMessage = validationMessage.GetValidationMessage(rule, EnityName, string.Empty);
			}

			return true;
		}

		public void Format(IFormatRule formatRule)
		{
			formatRule.Format(EnityName);
		}
	}
}
