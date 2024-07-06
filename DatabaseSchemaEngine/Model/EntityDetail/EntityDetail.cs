using DatabaseSchemaEngine.Validator;
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Model.EntityDetail
{
	using DatabaseSchemaEngine.Formatter;
	public class EntityDetail : IEntityDetail
	{
		public int Id { get; set; }

		public EntityDetail(int id,string enityName, List<AttributeDetail> attributes)
		{
			Id = id;
			EntityName = enityName;
			Attributes = attributes;
		}

		public string EntityName { get; set; }

		public List<AttributeDetail> Attributes { get; set; }

		public bool Validate(IValidationRule rule, IValidationMessageProvider validationMessage, out string errorMessage)
		{
			errorMessage = string.Empty;
			
			if (!rule.IsValid(EntityName))
			{
				errorMessage = validationMessage.GetValidationMessage(rule, EntityName, string.Empty);
				return false;
			}

			return true;
		}

		public void Format(IFormatRule formatRule)
		{
			EntityName = formatRule.Format(EntityName);
		}
	}
}
