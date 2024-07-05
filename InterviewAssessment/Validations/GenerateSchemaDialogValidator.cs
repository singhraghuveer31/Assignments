namespace DomainModelEditor.Validations
{
	using Constants;
    using DatabaseSchemaEngine.Lookup;

    /// <summary>
    /// Validations for GenerateSchemaDialog window.
    /// </summary>
    public static class GenerateSchemaDialogValidator
	{
		public static bool IsSelectedDataBaseSchemaValid(ILookup selectedValue) 
		{
			return selectedValue != null && selectedValue.Code != null;
		}
	}
}
