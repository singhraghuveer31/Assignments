using System.Collections.Generic;
using Autofac;
using DomainModelEditor.Model;
using DomainModelEditor.Model.Entities;
using DomainModelEditor.ViewModel;

namespace DomainModelEditor.UserControl
{
    public class EntityViewModel : BaseViewModel<EntityViewModel>, IEntityViewModel
    {
        private IDomainModelMetadataContext _domainModelMetadataContext;

        private IDomainModelMetadataContext DomainModelMetadataContext
        {
            get
            {
                if (_domainModelMetadataContext == null)
                    _domainModelMetadataContext = DependencyInjectionScope.Resolve<IDomainModelMetadataContext>();

                return _domainModelMetadataContext;
            }
        }

        private Model.Entities.Entity _entity;

        public Model.Entities.Entity Entity
        {
            get => _entity;
            set
            {
                _entity = value;

                RefreshAttributes();

                NotifyPropertyChanged();

                X = _entity.Coords.X;
                Y = _entity.Coords.Y;
            }
        }

        private void RefreshAttributes()
        {
            _attributes = new List<Attribute>();
            _attributes.AddRange(_entity.Attributes);

            NotifyPropertyChanged(nameof(IEntityViewModel.Attributes));
        }

        public string Name
        {
            get => Entity.Name;
            set
            {
                Entity.Name = value;
                NotifyPropertyChanged();
            }
        }

        public int X
        {
            get => Entity.Coords.X;
            set
            {
                Entity.Coords.X = value;
                NotifyPropertyChanged();
            }
        }

        public int Y
        {
            get => Entity.Coords.Y;
            set
            {
                Entity.Coords.Y = value;
                NotifyPropertyChanged();
            }
        }

        private List<Attribute> _attributes;
        public IReadOnlyList<Attribute> Attributes => _attributes;

        public string AutomationId => $"Entity<{Entity.Name}>";

        public EntityViewModel(ILifetimeScope dependencyInjectionScope)
        {
            DependencyInjectionScope = dependencyInjectionScope;
        }

        public int Save()
        {
            Logger.Debug("Saving state for Entity: {entityId} - {entityName}", Entity.Id, Entity.Name);

            return DomainModelMetadataContext.SaveChanges();
        }

        public Attribute AddNewAttribute(string attributeName, DataType dataType)
        {
            Logger.Debug(
                $"Adding new attribute: {{attributeName}} ({dataType}) to entity: {{entityId}} - {{entityName}}",
                attributeName, dataType,
                Entity.Id, Entity.Name);

            var newAttribute = new Attribute
            {
                Name = attributeName,
                DataType = dataType,
                Entity = Entity
            };

            DomainModelMetadataContext.Attributes.Add(newAttribute);
            DomainModelMetadataContext.SaveChanges();

            RefreshAttributes();

            return newAttribute;
        }
    }
}