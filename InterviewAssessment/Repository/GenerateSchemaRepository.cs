using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Lookup;
using DomainModelEditor.Constants;
using System.Collections.Generic;

namespace DomainModelEditor.Repository
{
    /// <summary>
    /// Repository for Generate Schema module.
    /// </summary>
    public class GenerateSchemaRepository : IGenerateSchemaRepository
    {
        public List<ILookup> GetNamingConventions()
        {
            var type = nameof(NamingConventionValues);
            return new List<ILookup>
            {
                new Lookup{ Name = "Capital Letter First", Code = $"{NamingConventionValues.CapitalCaseFirst}", TypeName = type },
                new Lookup{Name = "Camel Case", Code = $"{NamingConventionValues.CamelCase}", TypeName = type },
                new Lookup{Name = "Upper Case", Code = $"{NamingConventionValues.UpperCase}", TypeName = type },
                new Lookup{Name = "Lower Case", Code = $"{NamingConventionValues.LowerCase}", TypeName = type }
            };
        }

        public List<ILookup> GetSchemaGenerationOptions()
        {
            var typeName = nameof(SchemaGenerationOptionValues);
            return new List<ILookup>
            {
                new Lookup{ Name = "Replace space with underscore", Code = $"{SchemaGenerationOptionValues.ReplaceSpaceWithUnderScore}", TypeName = typeName },
                new Lookup{Name = "Dummy value", Code = $"{SchemaGenerationOptionValues.AnotherOption}", TypeName = typeName }
            };
        }

        public List<ILookup> GetTargetFrameworks()
        {
            var typeName = nameof(TargetDatabaseFrameworkValues);
            return new List<ILookup>
            {
                new Lookup{ Name = SchemaGenrationConstant.SFCDB, Code = $"{TargetDatabaseFrameworkValues.SFCDB}", TypeName = typeName },
                new Lookup{Name = SchemaGenrationConstant.SQLITE, Code = $"{TargetDatabaseFrameworkValues.SQLite}", TypeName = typeName }
            };
        }
    }
}
