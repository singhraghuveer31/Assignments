using System;
using System.Windows;
using Autofac;
using DomainModelEditor.Behaviour;
using DomainModelEditor.UserControl;
using DomainModelEditor.View.Dialog;
using DomainModelEditor.ViewModel;
using Serilog;

namespace DomainModelEditor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILifetimeScope DependencyInjectionScope { get; }

        private new IMainWindowViewModel DataContext
        {
            get => base.DataContext as IMainWindowViewModel;
            set => base.DataContext = value;
        }

        private ILogger _logger = null;

        protected ILogger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = Log.Logger.ForContext<MainWindow>();

                return _logger;
            }
        }

        private Entity CurrentEntity { get; set; }

        public MainWindow(ILifetimeScope dependencyInjectionScope)
        {
            DependencyInjectionScope = dependencyInjectionScope;

            InitializeComponent();
        }

        private void AddEntity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var popup = new AddEntityDialog();

                if (!string.IsNullOrWhiteSpace(popup.EntityName))
                {
                    var randomNrGenerator = new Random();
                    var X = randomNrGenerator.Next((int)EditorCanvas.ActualWidth - 80);
                    var Y = randomNrGenerator.Next((int)EditorCanvas.ActualHeight - 50);

                    DataContext.AddEntity(popup.EntityName, X, Y);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in {handler} event handler.", nameof(AddEntity_Click));
            }
        }

        private void GenerateSchema_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                var popup = DependencyInjectionScope.Resolve<GenerateSchemaDialog>();
                (popup.DataContext as GenerateSchemaDialogViewModel).Entities = DataContext.Entities;
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in {handler} event handler.", nameof(AddEntity_Click));
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                IDraggingBehaviour draggingBehaviour = DependencyInjectionScope.Resolve<IDraggingBehaviour>();
                draggingBehaviour.ApplyToContainer(this);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in {handler} event handler.", nameof(Window_Initialized));
            }
        }
    }
}