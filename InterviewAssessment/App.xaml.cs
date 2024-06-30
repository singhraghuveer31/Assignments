using Serilog;
using System;
using System.Windows;
using DomainModelEditor.ViewModel;
using DomainModelEditor.Util;

namespace DomainModelEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ILogger Logger { get; }

        public App()
        {
            ConfigureLogger();

            Logger = Log.Logger.ForContext<App>();

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private void ConfigureLogger()
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            var x = new LoggerConfiguration()
                              .ReadFrom.AppSettings();

            Log.Logger = x
                              .CreateLogger();

            Serilog.Debugging.SelfLog.Disable();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.IsTerminating)
                Logger.Error("Application is terminating due to an unhandled exception.");

            if (args.ExceptionObject is Exception exception)
            {
                Logger.Error(exception, "Unhandled exception");
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Logger.Information("Application is starting.");
            
            BootStrapper.Start();

            var window = new View.MainWindow(BootStrapper.DependencyInjectionScope)
            {
                DataContext = BootStrapper.Resolve<IMainWindowViewModel>()
            };

            window.Show();
            Logger.Information("Application started.");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Logger.Information("Application is exitting.");
            BootStrapper.Stop();
        }
    }
}
