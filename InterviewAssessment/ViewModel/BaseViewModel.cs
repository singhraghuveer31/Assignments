using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using Serilog;

namespace DomainModelEditor.ViewModel
{
    public abstract class BaseViewModel<TImplementation> : IViewModel
    {
        private ILogger _logger = null;

        public ILifetimeScope DependencyInjectionScope { get; protected set; }

        protected ILogger Logger
        {
            get {
                if (_logger == null)
                    _logger = Log.Logger.ForContext<TImplementation>();

                return _logger;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
