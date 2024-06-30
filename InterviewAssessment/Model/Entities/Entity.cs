using System.Collections.Generic;

namespace DomainModelEditor.Model.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Coords Coords { get; set; }
        public virtual List<Attribute> Attributes { get; set; }
    }
}
