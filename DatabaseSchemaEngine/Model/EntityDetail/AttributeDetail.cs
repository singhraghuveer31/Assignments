using DatabaseSchemaEngine.Formatter;
using DatabaseSchemaEngine.Validator;
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	public class AttributeDetail : IAttributeDetail
	{
		public int Id { get; set; }

		private readonly string entityName;

		public AttributeDetail(int id, string type, string name, string entityName)
		{
			Id = id;
			Name = name;
			this.entityName = entityName;
			Type = type;
		}
		public string Type { get; set; }

		public string Name { get; set; }

		public bool Validate(IValidationRule rule, IValidationMessageProvider validationMessage, out string errorMessage)
		{
			errorMessage = string.Empty;
			if (!rule.IsValid(Name))
			{
				errorMessage = validationMessage.GetValidationMessage(rule, entityName, Name);
				return false;
			}

			return true;
		}

		public void Format(IFormatRule formatRule)
		{
			Name = formatRule.Format(Name);
		}
	}
}
