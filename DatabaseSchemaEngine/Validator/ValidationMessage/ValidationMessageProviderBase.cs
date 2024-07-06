using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;

namespace DatabaseSchemaEngine.Validator.ValidationMessage
{
    public abstract class ValidationMessageProviderBase : IValidationMessageProvider
    {
        #region Fields
        
        private readonly string tableNameConvention;
        private readonly string columnNameConvention;
        private readonly string tableNameConvForAtStart;
        private readonly string columnNameConvForAtStart;

        #endregion

        public ValidationMessageProviderBase(string tableNameConvention, string columnNameConvention)
        {
            this.tableNameConvention = tableNameConvention;
            this.columnNameConvention = columnNameConvention;
            tableNameConvForAtStart = char.ToUpperInvariant(tableNameConvention[0]) + tableNameConvention.Substring(1);
            columnNameConvForAtStart = char.ToUpperInvariant(columnNameConvention[0]) + columnNameConvention.Substring(1);
        }

        #region Abstract Methods

        protected abstract int GetColumnMaxLength();

        protected abstract int GetColumnMinLength();

        protected abstract int GetTableLength();

        protected abstract string GetAllowedSpecialCharacters();

        #endregion

        #region Protected Virtual Methods
        protected virtual string GetPrefixValidationMessageForTableName(string tableName, Enum.PrefixConventionValues conventionValue)
        {
            return string.Format(ValidationConstant.TableNamePrefixValidationMessage, tableNameConvForAtStart, tableName, conventionValue.ToString());
        }

        protected virtual string GetPrefixValidationMessageForColumn(string columnName, string tableName, Enum.PrefixConventionValues conventionValue)
        {

            return string.Format(ValidationConstant.ColumnPrefixValidationMessage, columnNameConvForAtStart, columnName, tableNameConvention, tableName, conventionValue.ToString());
        }

        protected virtual string GetColumnNameAlreadyExistsValidationMessage(string columnName, string tableName)
        {
            return string.Format(ValidationConstant.ColumnNameAlreadyExistsValidationMessage, columnNameConvForAtStart,columnName, tableNameConvention, tableName);
        }

        protected virtual string GetColumnLengthValidationMessage(string columnName, string tableName)
        {
            return string.Format(ValidationConstant.ColumnLengthValidationMessage, columnNameConvention, columnName, tableNameConvention, tableName, GetColumnMinLength(), GetColumnMaxLength());
        }

        protected virtual string GetColumnSpecialCharValidationMessage(string columnName, string tableName)
        {
            return string.Format(ValidationConstant.ColumnSpecialCharValidationMessage, columnNameConvention, columnName, tableNameConvention, tableName, GetAllowedSpecialCharacters());
        }

        protected virtual string GetTableNameAlreadyExistsValidationMessage(string tableName)
        {
            return string.Format(ValidationConstant.TableNameAlreadyExistsValidationMessage, tableNameConvForAtStart, tableName);
        }

        protected virtual string GetTableNameLengthValidationMessage(string tableName)
        {
            return string.Format(ValidationConstant.TableNameLengthValidation, tableNameConvention, tableName, GetTableLength());
        }

        protected virtual string GetTableNameSpecialCharValidationMessage(string tableName)
        {
            return string.Format(ValidationConstant.TableNameSpecialCharValidationMessage, tableNameConvention, tableName, string.Join(",", ValidationConstant.SFCDBAllowedSpecialChars));
        }

        #endregion

        #region Public Methods

        public string GetValidationMessage<T>(T rule, string entityName, string attributeName) where T : IValidationRule
        {
            bool isEntityValidation = string.IsNullOrWhiteSpace(attributeName);

            switch (rule.GetType().Name)
            {
                case nameof(LengthValidationRule):
                    return isEntityValidation ? GetTableNameLengthValidationMessage(entityName) : GetColumnLengthValidationMessage(attributeName, entityName);

                case nameof(PrefixValidationRule):
                    var prefixValidationRule = rule as PrefixValidationRule;

                    if (prefixValidationRule == null)
                        break;

                    return isEntityValidation ? GetPrefixValidationMessageForTableName(entityName, prefixValidationRule.PrefixConvention) : GetPrefixValidationMessageForColumn(attributeName, entityName, prefixValidationRule.PrefixConvention);

                case nameof(SpecialCharacterValidationRule):
                    return isEntityValidation ? GetTableNameSpecialCharValidationMessage(entityName) : GetColumnSpecialCharValidationMessage(attributeName, entityName);

                case nameof(UniqueNameValidationRule):
                    return isEntityValidation ? GetTableNameAlreadyExistsValidationMessage(entityName) : GetColumnNameAlreadyExistsValidationMessage(attributeName, entityName);

            }

            return string.Empty;
        }
        #endregion
    }
}