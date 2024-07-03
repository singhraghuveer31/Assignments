namespace DatabaseSchemaEngine.Model.EntityDetail
{
	public class EntityDetail : IEntityDetail
	{
		public EntityDetail(string enityName, List<AttributeDetail> attributes)
		{
			EnityName = enityName;
			Attributes = attributes;
		}

		public string EnityName { get; set; }

		public List<AttributeDetail> Attributes { get; set; }
	}
}
