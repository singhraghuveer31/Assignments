using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using DomainModelEditor.Model;
using DomainModelEditor.UserControl;
using Microsoft.EntityFrameworkCore;
using ModelEntities = DomainModelEditor.Model.Entities;

namespace DomainModelEditor.ViewModel
{
    public class MainWindowViewModel : BaseViewModel<MainWindowViewModel>, IMainWindowViewModel
    {
        IDomainModelMetadataContext DomainModelMetadataDbContext;

        public MainWindowViewModel(ILifetimeScope dependencyInjectionScope)
        {
            DependencyInjectionScope = dependencyInjectionScope;

            DomainModelMetadataDbContext = DependencyInjectionScope.Resolve<IDomainModelMetadataContext>();

            RefreshEntities();
        }

        private void RefreshEntities()
        {
            var entities = DomainModelMetadataDbContext.Entities
                .Include(e => e.Attributes)
                .Include(e => e.Coords)
                .ToList();
            var entityViewModels = new List<IEntityViewModel>();

            foreach (var entity in entities)
            {
                var entityViewModel = DependencyInjectionScope.Resolve<IEntityViewModel>();
                entityViewModel.Entity = entity;
                entityViewModels.Add(entityViewModel);

                entityViewModel.PropertyChanged += EntityViewModel_PropertyChanged;
            }

            Entities = entityViewModels;
            NotifyPropertyChanged();
        }

        private void EntityViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ((IEntityViewModel) sender).Save();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in {handler} event handler.", nameof(EntityViewModel_PropertyChanged));
            }
        }

        private List<IEntityViewModel> _entities;

        public List<IEntityViewModel> Entities
        {
            get { return _entities; }
            set
            {
                _entities = value;
                NotifyPropertyChanged();
            }
        }

        public void AddEntity(string name, int x, int y)
        {
            DomainModelMetadataDbContext.Entities.Add(CreateEntity(name, x, y));
            DomainModelMetadataDbContext.SaveChanges();

            RefreshEntities();
        }

        private ModelEntities.Entity CreateEntity(string name, int x, int y)
        {
            var entity = new ModelEntities.Entity {Name = name};
            var coords = new ModelEntities.Coords {Entity = entity, X = x, Y = y};
            entity.Coords = coords;
            return entity;
        }
    }
}