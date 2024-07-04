using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Extension;
using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using DatabaseSchemaEngine.Validator.ValidationMessage;

namespace DatabaseSchemaEngine.Validator.ValidatorProvider.SFCDB
{
	internal class SFCDBSchemaValidatorProvider : IValidatorProvider
	{
		private readonly IValidationMessageProvider validationMessageProvider;

		public SFCDBSchemaValidatorProvider(IValidationMessageProvider validationMessageProvider)
		{
			this.validationMessageProvider = validationMessageProvider;
		}

		public void Register()
		{
			RegisterEntityValidators();
			RegisterAttributeValidators();
		}

		private static void RegisterEntityValidators()
		{
			var tablesHash = new HashSet<string>();
			RegisterEntityValidator(new LengthValidationRule(ValidationConstant.SFCDBDataStoreLength, ValidationConstant.SFCDBDataStoreLength));
			RegisterEntityValidator(new LengthValidationRule(ValidationConstant.SFCDBDataStoreLength, ValidationConstant.SFCDBDataStoreLength));
			RegisterEntityValidator(new PrefixValidationRule(Enum.PrefixConventionValues.Letter));
			RegisterEntityValidator(new SpecialCharacterValidationRule(ValidationConstant.SFCDBAllowedSpecialChars));
			RegisterEntityValidator(new UniqueNameValidationRule(Enum.TypeName.Entity));
		}

		private static void RegisterAttributeValidators()
		{
			var columnsHash = new HashSet<string>();
			RegisterAttributeValidator(new LengthValidationRule(ValidationConstant.SFCDBDataStoreLength, ValidationConstant.SFCDBDataStoreLength));
			RegisterAttributeValidator(new PrefixValidationRule(Enum.PrefixConventionValues.Letter));
			RegisterAttributeValidator(new SpecialCharacterValidationRule(ValidationConstant.SFCDBAllowedSpecialChars));
			RegisterAttributeValidator(new UniqueNameValidationRule(Enum.TypeName.Attibute));
		}

		private static void RegisterEntityValidator(IValidationRule rule)
		{
			ValidatorRegisterExtension.RegisterValidator(nameof(IEntityDetail), rule);
		}

		private static void RegisterAttributeValidator(IValidationRule rule)
		{
			ValidatorRegisterExtension.RegisterValidator(nameof(IAttributeDetail), rule);
		}

		private static void RegisterValidator(string typeName, IValidationRule rule)
		{
			ValidatorRegisterExtension.RegisterValidator(typeName, rule);
		}

		public List<string> Validate(List<IEntityDetail> entityDetails)
		{
			SchemaMappingCache.EntityNameValidationCache.Clear();

			var errorList = new List<string>();
			foreach (var entity in entityDetails) 
			{
				Validator.Validate(entity, errorList, validationMessageProvider);

				SchemaMappingCache.AttributeNameValidationCache.Clear();
				foreach (var attribute in entity.Attributes)
				{
					Validator.Validate(attribute, errorList, validationMessageProvider);
				}
			}

			return errorList;
		}
	}
}
