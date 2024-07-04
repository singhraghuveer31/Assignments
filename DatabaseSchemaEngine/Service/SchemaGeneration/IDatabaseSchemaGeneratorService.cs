﻿using DatabaseSchemaEngine.Model.EntityDetail;

namespace DatabaseSchemaEngine.Service.SchemaGeneration
{
    /// <summary>
    /// Service interface to generate database schema.
    /// </summary>
    public interface IDatabaseSchemaGeneratorService
    {
        /// <summary>
        /// Generates database schema.
        /// </summary>
        void GenerateDatabaseSchema(string targetFramework, IEnumerable<IEntityDetail> entityDetails);

		/// <summary>
		/// Contains output for schema generation service.
		/// </summary>
		ISchemaGenerationOutput Output { get; protected set; }
    }
}
