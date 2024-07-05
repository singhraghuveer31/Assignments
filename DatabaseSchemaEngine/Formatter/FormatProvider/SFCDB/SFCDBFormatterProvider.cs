using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Formatter.SyntaxFormatRules;
using DatabaseSchemaEngine.Lookup;
using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Formatter.FormatProvider.SFCDB
{
	internal class SFCDBFormatterProvider : IFormatterProvider
	{
		public IEnumerable<ILookup> SchemaGenerationOptions { get; }

		public SFCDBFormatterProvider(IEnumerable<ILookup> schemaGenerationOptions)
		{
			SchemaGenerationOptions = schemaGenerationOptions;
			Register();
		}

		private void Register()
		{
			foreach (var option in SchemaGenerationOptions)
			{
				if (option.TypeName == nameof(NamingConventionValues))
				{
					Formatter.RegisterFormatterFor(nameof(IEntityDetail), new NamingConventionRule(System.Enum.Parse<NamingConventionValues>(option.Code)));
				}
				else if (option.TypeName == nameof(SchemaGenerationOptionValues))
				{
					var schemaGenerationOption = System.Enum.Parse<SchemaGenerationOptionValues>(option.Code);

					if (schemaGenerationOption == SchemaGenerationOptionValues.ReplaceSpaceWithUnderScore) 
					{
						Formatter.RegisterFormatterFor(nameof(IEntityDetail), new ReplaceCharacterRule(' ', '_'));
					}
				};
			}
		}

		public void Format(List<IEntityDetail> entityDetails)
		{
			foreach (IEntityDetail entityDetail in entityDetails)
			{
				Formatter.Format(entityDetail);

				foreach (var attribute in entityDetail.Attributes)
				{
					Formatter.Format(attribute);
				}
			}
		}
	}
}
