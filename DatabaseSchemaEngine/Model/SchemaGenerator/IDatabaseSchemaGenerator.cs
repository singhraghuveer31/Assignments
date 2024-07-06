namespace DatabaseSchemaEngine.Model.SchemaGenerator
{
	using DatabaseSchemaEngine.Model.EntityDetail;
	using System.Collections.Generic;
    /// <summary>
    /// Defines methods for schema generation.
    /// </summary>
    public interface IDatabaseSchemaGenerator
    {
		/// <summary>
		/// Generates database schema.
		/// </summary>
		/// <param name="entityDetails"></param>
        public void GenerateDatabaseSchema(IEnumerable<IEntityDetail> entityDetails);
    }
}
