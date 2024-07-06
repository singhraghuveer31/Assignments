using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Formatter.SyntaxFormatRules;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Formatter.FormatProvider
{
	public abstract class FormatterProviderBase : IFormatterProvider
	{
		public FormatterProviderBase(IEnumerable<ILookup> schemaGenerationOptions)
		{
			SchemaGenerationOptions = schemaGenerationOptions;
			Register();
		}

		protected IEnumerable<ILookup> SchemaGenerationOptions { get; }

		protected virtual void Register()
		{
			foreach (var option in SchemaGenerationOptions)
			{
				if (option.TypeName == nameof(NamingConventionValues))
				{
					Formatter.RegisterFormatterFor(nameof(EntityDetail), new NamingConventionRule(System.Enum.Parse<NamingConventionValues>(option.Code)));
					Formatter.RegisterFormatterFor(nameof(AttributeDetail), new NamingConventionRule(System.Enum.Parse<NamingConventionValues>(option.Code)));
				}
				else if (option.TypeName == nameof(SchemaGenerationOptionValues))
				{
					var schemaGenerationOption = System.Enum.Parse<SchemaGenerationOptionValues>(option.Code);

					if (schemaGenerationOption == SchemaGenerationOptionValues.ReplaceSpaceWithUnderScore)
					{
						Formatter.RegisterFormatterFor(nameof(EntityDetail), new ReplaceCharacterRule(' ', '_'));
						Formatter.RegisterFormatterFor(nameof(AttributeDetail), new ReplaceCharacterRule(' ', '_'));
					}
				};
			}
		}

		public void Format(List<IEntityDetail> entityDetails)
		{
			foreach (IEntityDetail entityDetail in entityDetails)
			{
				entityDetail.Format();

				foreach (var attribute in entityDetail.Attributes)
				{
					attribute.Format();
				}
			}
		}
	}
}