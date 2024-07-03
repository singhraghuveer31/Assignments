namespace DomainModelEditor.Validations
{
	using Constants;
	
	/// <summary>
	/// Validations for GenerateSchemaDialog window.
	/// </summary>
	public static class GenerateSchemaDialogValidator
	{
		public static bool IsSelectedDataBaseSchemaValid(string selectedValue) 
		{
			return selectedValue != null && !selectedValue.Equals(SchemaGenrationConstant.DEFAULT);
		}
	}
}
