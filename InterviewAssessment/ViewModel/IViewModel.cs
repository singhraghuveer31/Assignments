
using Autofac;
using System.ComponentModel;

namespace DomainModelEditor.ViewModel
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ILifetimeScope DependencyInjectionScope { get; }
    }
}
