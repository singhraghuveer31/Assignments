namespace DatabaseSchemaEngine.Lookup
{
	public interface ILookup
	{
		string Code { get; set; }
		string Name { get; set; }

		string TypeName { get; set; }
	}
}