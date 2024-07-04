using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;

namespace DatabaseSchemaEngine.Validator.ValidationMessage
{
	public class SFCDBValidationMessageProvider : IValidationMessageProvider
	{
		public static string GetDataStoreLengthValidationMessage(string dataStoreName) 
		{
			return $"Data store: {dataStoreName} length should be {ValidationConstant.SFCDBDataStoreLength}.";
		}

		public static string GetPropLengthValidationMessage(string propName, string dataStoreName) 
		{
			return $"Prop name: {propName} length for data store {dataStoreName} should be between {ValidationConstant.SFCDBPropMinLength} and {ValidationConstant.SFCDBPropMaxLength} characters.";
		}

		public static string GetDataStoreAlreadyExistsValidationMessage(string dataStoreName)
		{
			return $"Data store with name {dataStoreName} already exists.";
		}

		public static string GetPropAlreadyExistsValidationMessage(string propName, string dataStoreName) 
		{
			return $"Prop with name {propName} already exists for data store {dataStoreName}.";
		}

		public static string GetPrefixValidationMessageForProp(string propName, string dataStoreName, Enum.PrefixConventionValues conventionValue)
		{ 

			return $"Prop: {propName} of data store {dataStoreName} should start with a {conventionValue}"; 
		}

		public static string GetPrefixValidationMessageForDataStore(string dataStore, Enum.PrefixConventionValues conventionValue)
		{
			return $"Data store {dataStore} should start with a {conventionValue}";
		}

		public static string GetDataStoreSpecialCharValidationMessage(string dataStoreName)
		{
			return $"Allowed special characters for data store: {dataStoreName} are : {string.Join(",",ValidationConstant.SFCDBAllowedSpecialChars)}.";
		}

		public static string GetPropSpecialCharValidationMessage(string propName, string dataStoreName)
		{
			return $"Allowed special characters for prop: {propName} of data store: {dataStoreName} are : {string.Join(",", ValidationConstant.SFCDBAllowedSpecialChars)}.";
		}

		public string GetValidationMessage<T>(T rule, string entityName, string attributeName) where T : IValidationRule
		{
			bool isEntityValidation = string.IsNullOrWhiteSpace(attributeName);

			switch (rule.GetType().Name) 
			{
				case nameof(LengthValidationRule):
					return isEntityValidation ? GetDataStoreLengthValidationMessage(entityName) : GetPropLengthValidationMessage(attributeName, entityName);
				
				case nameof(PrefixValidationRule):
					var prefixValidationRule = rule as PrefixValidationRule;

					if (prefixValidationRule == null) 
						break;

					return isEntityValidation ? GetPrefixValidationMessageForDataStore(entityName, prefixValidationRule.PrefixConvention) : GetPrefixValidationMessageForProp(attributeName, entityName, prefixValidationRule.PrefixConvention);
				
				case nameof(SpecialCharacterValidationRule):
					return isEntityValidation ? GetDataStoreSpecialCharValidationMessage(entityName) : GetPropSpecialCharValidationMessage(attributeName, entityName);

				case nameof(UniqueNameValidationRule):
					return isEntityValidation ? GetDataStoreAlreadyExistsValidationMessage(entityName) : GetPropAlreadyExistsValidationMessage(attributeName, entityName);
			
			}

			return string.Empty;
		}
	}
}
