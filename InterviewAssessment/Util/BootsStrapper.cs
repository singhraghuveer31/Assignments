using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using DatabaseSchemaEngine.Repository;
using DatabaseSchemaEngine.Service.SchemaGeneration;
using DomainModelEditor.Behaviour;
using DomainModelEditor.Model;
using DomainModelEditor.Properties;
using DomainModelEditor.View.Dialog;
using DomainModelEditor.ViewModel;
using Serilog;

namespace DomainModelEditor.Util
{
    public static class BootStrapper
    {
        private static ILogger _logger = null;

        private static ILogger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = Log.Logger.ForContext(typeof(BootStrapper));

                return _logger;
            }
        }

        public static ILifetimeScope DependencyInjectionScope { get; private set; }

        #region Guard Clauses

        private static bool IsAlreadyBootstrappedGuard()
        {
            if (DependencyInjectionScope != null)
            {
                Logger.Warning("Boostrapping has already been performed.");
                return true;
            }

            return false;
        }

        private static void IsNotStartedGuard()
        {
            if (DependencyInjectionScope == null) throw new Exception(Resources.Bootstrapping_Not_Performed);
        }

        #endregion Guard Clauses

        public static void Start()
        {
            Logger.Information("Starting bootstrap process.");

            if (IsAlreadyBootstrappedGuard()) return;

            SetUpContainer();

            Logger.Information("Bootstrap process finished.");
        }

        public static void Stop()
        {
            DependencyInjectionScope.Dispose();
        }

        private static void SetUpContainer()
        {
            Logger.Debug("Setting up dependency injection container.");

            var builder = new ContainerBuilder();

            BuildupContainer(builder);

            DependencyInjectionScope = builder.Build();

            Logger.Debug("Finished setting up dependency container.");
        }

        private static void BuildupContainer(ContainerBuilder builder)
        {
            Logger.Debug(
                "Registering all view models that implement the {viewModelInterface} interface for assembly: {assembly}",
                typeof(IViewModel).FullName, Assembly.GetExecutingAssembly().FullName);


            var assemblies = new[] {Assembly.GetExecutingAssembly()};

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(IViewModel).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            Logger.Debug("View models registered.");

            Logger.Debug("Registering {dataContext} as {dataContextInterface}",
                typeof(DomainModelMetadataContext).FullName, typeof(IDomainModelMetadataContext).FullName);
            builder.RegisterInstance<IDomainModelMetadataContext>(new DomainModelMetadataContext());

            Logger.Debug("Registering logger.");
            builder.RegisterInstance(Log.Logger);

            Logger.Debug("Registering dragging behaviour.");
            builder.RegisterInstance<IDraggingBehaviour>(new DraggingBehaviour());

			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            var databaseSchemaGenerationService = new DatabaseSchemaGenerationService(Logger);
			builder.RegisterInstance<IDatabaseSchemaGeneratorService>(databaseSchemaGenerationService);
			builder.RegisterType<GenerateSchemaDialog>().
			WithParameters(new List<Parameter>
			{
				new TypedParameter(typeof(IDatabaseSchemaGeneratorService), databaseSchemaGenerationService),
			});
		}

        public static T Resolve<T>()
        {
            Logger.Debug("Resolving: {resolveTypeFullName}", typeof(T).FullName);

            IsNotStartedGuard();

            return DependencyInjectionScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            Logger.Debug("Resolving: {resolveTypeFullName} - with parameters.", typeof(T).FullName);

            IsNotStartedGuard();

            return DependencyInjectionScope.Resolve<T>(parameters);
        }
    }
}