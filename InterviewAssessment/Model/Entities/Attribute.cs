namespace DomainModelEditor.Model.Entities
{
    public class Attribute
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public string Name { get; set; }
        public DataType? DataType { get; set; }
    }
}