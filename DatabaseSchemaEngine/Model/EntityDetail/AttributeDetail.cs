namespace DatabaseSchemaEngine.Model.EntityDetail
{
	public class AttributeDetail
	{
		public AttributeDetail(string type, string name)
		{
			Name = name;
			Type = type;
		}
		public string Type { get; set; }

		public string Name { get; set; }
	}
}
