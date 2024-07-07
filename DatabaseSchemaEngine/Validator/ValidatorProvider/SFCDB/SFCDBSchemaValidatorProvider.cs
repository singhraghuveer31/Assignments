using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Extension;
using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider.SFCDB
{
	internal class SFCDBSchemaValidatorProvider : SchemaValidatorProviderBase
	{
		public SFCDBSchemaValidatorProvider(IValidationMessageProvider validationMessageProvider) : base(validationMessageProvider)
		{
		}

		protected override void Register()
		{
			RegisterEntityValidators();
			RegisterAttributeValidators();
		}

		private static void RegisterEntityValidators()
		{
			var tablesHash = new HashSet<string>();
			RegisterEntityValidator(new LengthValidationRule(ValidationConstant.SFCDBDataStoreLength, ValidationConstant.SFCDBDataStoreLength));
			RegisterEntityValidator(new PrefixValidationRule(Enum.PrefixConventionValues.Letter));
			RegisterEntityValidator(new SpecialCharacterValidationRule(ValidationConstant.SFCDBAllowedSpecialChars));
			RegisterEntityValidator(new UniqueNameValidationRule(Enum.TypeNameValues.Entity));
		}

		private static void RegisterAttributeValidators()
		{
			var columnsHash = new HashSet<string>();
			RegisterAttributeValidator(new LengthValidationRule(ValidationConstant.SFCDBPropMinLength, ValidationConstant.SFCDBPropMaxLength));
			RegisterAttributeValidator(new PrefixValidationRule(Enum.PrefixConventionValues.Letter));
			RegisterAttributeValidator(new SpecialCharacterValidationRule(ValidationConstant.SFCDBAllowedSpecialChars));
			RegisterAttributeValidator(new UniqueNameValidationRule(Enum.TypeNameValues.Attibute));
		}

		private static void RegisterEntityValidator(IValidationRule rule)
		{
			ValidatorRegisterExtension.RegisterValidator(nameof(EntityDetail), rule);
		}

		private static void RegisterAttributeValidator(IValidationRule rule)
		{
			ValidatorRegisterExtension.RegisterValidator(nameof(AttributeDetail), rule);
		}

		private static void RegisterValidator(string typeName, IValidationRule rule)
		{
			ValidatorRegisterExtension.RegisterValidator(typeName, rule);
		}
	}
}
