using DatabaseSchemaEngine.Lookup;
using System.Collections.Generic;

namespace DomainModelEditor.Repository
{
    public interface IGenerateSchemaRepository
    {
        List<ILookup> GetTargetFrameworks();

        List<ILookup> GetSchemaGenerationOptions();

        List<ILookup> GetNamingConventions();
    }
}
