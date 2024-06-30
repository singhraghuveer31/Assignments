using System.Collections.Generic;
using DomainModelEditor.ViewModel;
using ModelEntities = DomainModelEditor.Model.Entities;

namespace DomainModelEditor.UserControl
{
    public interface IEntityViewModel : IViewModel
    {
        ModelEntities.Entity Entity { get; set; }

        string Name { get; set; }

        int X { get; set; }

        int Y { get; set; }

        IReadOnlyList<ModelEntities.Attribute> Attributes { get; }

        string AutomationId { get; }

        int Save();

        ModelEntities.Attribute AddNewAttribute(string attributeName, ModelEntities.DataType dataType);
    }
}