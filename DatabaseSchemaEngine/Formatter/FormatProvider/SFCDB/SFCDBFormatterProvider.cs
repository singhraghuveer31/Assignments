using DatabaseSchemaEngine.Lookup;

namespace DatabaseSchemaEngine.Formatter.FormatProvider.SFCDB
{
	internal class SFCDBFormatterProvider : FormatterProviderBase, IFormatterProvider
	{
		public SFCDBFormatterProvider(IEnumerable<ILookup> schemaGenerationOptions) : base(schemaGenerationOptions)
		{
		}
	}
}
