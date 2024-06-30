namespace DomainModelEditor.Model.Entities
{
    public class Coords
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public virtual Entity Entity { get; set; }
    }
}