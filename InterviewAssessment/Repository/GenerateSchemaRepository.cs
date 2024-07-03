using DomainModelEditor.Constants;
using System.Collections.Generic;

namespace DomainModelEditor.Repository
{
	/// <summary>
	/// Repository for Generate Schema module.
	/// </summary>
	public class GenerateSchemaRepository
	{
		public static List<string> GetTargetFrameworks() 
		{
			return new List<string>
			{
				SchemaGenrationConstant.DEFAULT,
				SchemaGenrationConstant.SFCDB,
				SchemaGenrationConstant.SQLITE
			};
		}
	}
}
