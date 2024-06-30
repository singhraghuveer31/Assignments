using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using Autofac;
using DomainModelEditor.Behaviour;
using DomainModelEditor.Model.Entities;
using DomainModelEditor.View.Dialog;
using Serilog;
using Attribute = DomainModelEditor.Model.Entities.Attribute;

namespace DomainModelEditor.UserControl
{
    /// <summary>
    /// Interaction logic for Entity.xaml
    /// </summary>
    public partial class Entity : System.Windows.Controls.UserControl
    {
        private static double WIDTH = 152;
        private static double TYPE_WIDTH = 60;
        static ILogger Logger { get; set; }

        const int AttributeRowHeight = 26;
        const int AttributeTextHeight = 26;
        const int MaximumAttributeNameLength = 255;

        private ILifetimeScope DependencyInjectionScope => DataContext.DependencyInjectionScope;

        private IDraggingBehaviour DraggingBehaviour = null;


        public new IEntityViewModel DataContext
        {
            get => base.DataContext as IEntityViewModel;
            set => base.DataContext = value;
        }

        private int AttributesDisplayedCount => EntityVisual.Children.Count - 2;

        public Entity()
        {
            Logger = Logger ?? Log.Logger.ForContext<Entity>();

            InitializeComponent();

            DataContextChanged += DataContextChangeHandler;
        }

        private void DataContextChangeHandler(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue == e.OldValue)
                {
                    return;
                }

                if (DraggingBehaviour == null)
                {
                    DraggingBehaviour = DependencyInjectionScope.Resolve<IDraggingBehaviour>();
                    DraggingBehaviour.ApplyToMovableElement(this);
                    DraggingBehaviour.DragFinished += UpdateCoordinates;
                }

                DataContext.PropertyChanged += DataContext_PropertyChanged;

                AutomationProperties.SetAutomationId(this, DataContext.AutomationId);

                UpdateAttributeLayout();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in {nameof(DataContextChangeHandler)}");
            }
        }

        private void UpdateCoordinates(object sender, DragFinishedEventArgs e)
        {
            if (e.DraggedElement == this)
            {
                if (DataContext.X == e.X && DataContext.Y == e.Y)
                {
                    return;
                }

                DataContext.X = e.X;
                DataContext.Y = e.Y;
                DataContext.Save();
            }
        }

        private void DataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == nameof(IEntityViewModel.Attributes))
                {
                    UpdateAttributeLayout();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in {nameof(DataContext_PropertyChanged)}");
            }
        }

        private void UpdateAttributeLayout()
        {
            var numberOfAttributes = DataContext.Attributes.Count;
            var attributesDisplayed = AttributesDisplayedCount;

            if (numberOfAttributes > attributesDisplayed)
            {
                for (var attributeNumberToAdd = attributesDisplayed;
                    attributeNumberToAdd < numberOfAttributes;
                    attributeNumberToAdd++)
                {
                    var attributeToAdd = DataContext.Attributes[attributeNumberToAdd];

                    var (newAttributeNameLabel, newAttributeTypeLabel) = CreateAttributeControls(attributeToAdd);

                    AddGridRow(newAttributeNameLabel, newAttributeTypeLabel);
                }
            }
        }

        public static (Control nameLabel, Control typeLabel) CreateAttributeControls(Attribute attributeToAdd)
        {
            var newAttributeNameLabel = new Label
            {
                Content = attributeToAdd.Name,
                Height = AttributeTextHeight,
                Width = WIDTH - TYPE_WIDTH
            };

            var newAttributeTypeLabel = new Label
            {
                Content = attributeToAdd.DataType.ToString(),
                Height = AttributeTextHeight,
                Width = TYPE_WIDTH
            };
            return (newAttributeNameLabel, newAttributeTypeLabel);
        }

        private void AddAttributeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var popup = new AddAttributeDialog();
                popup.ShowDialog();

                if (!string.IsNullOrWhiteSpace(popup.AttributeName))
                {
                    DataContext.AddNewAttribute(popup.AttributeName, DataType.String);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in {nameof(AddAttributeMenuItem_Click)}");
            }
        }

        private void AddAttributeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var newAttributeNameTextBox = new TextBox
                {
                    Text = "",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = WIDTH - TYPE_WIDTH,
                    Height = AttributeTextHeight - 4,
                    Margin = new Thickness(2, 0, 2, 0),
                    IsTabStop = true,
                    MaxLength = MaximumAttributeNameLength
                };

                var newAttributeTypeDropDown = new ComboBox
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = TYPE_WIDTH,
                    Height = AttributeTextHeight - 4,
                    Margin = new Thickness(2, 0, 2, 0),
                    Items = {DataType.String, DataType.Int},
                    SelectedValue = DataType.String
                };

                var grid = AddGridRow(newAttributeNameTextBox, newAttributeTypeDropDown);

                var attributeSaveAction = new EntityAttributeSaveAction
                {
                    Entity = this,
                    NewAttributeNameTextBox = newAttributeNameTextBox,
                    NewAttributeTypeDropDown = newAttributeTypeDropDown,
                    NewAttributeGrid = grid,
                    RowNumber = Grid.GetRow(grid)
                };

                newAttributeNameTextBox.KeyDown += attributeSaveAction.PerformAndDispose;
                newAttributeTypeDropDown.KeyDown += attributeSaveAction.PerformAndDispose;

                newAttributeNameTextBox.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in {nameof(AddAttributeButton_MouseLeftButtonDown)}");
            }
        }

        private Grid AddGridRow(Control attributeName, Control attributeType)
        {
            EntityDivider.Visibility = Visibility.Visible;

            EntityVisual.RowDefinitions.Add(new RowDefinition {Height = new GridLength(AttributeRowHeight)});

            var grid = SetGridRow(EntityVisual.RowDefinitions.Count - 1, attributeName, attributeType);

            EntityVisual.Height += AttributeRowHeight;

            Grid.SetRowSpan(EntityBorder, EntityVisual.RowDefinitions.Count);

            return grid;
        }

        public Grid SetGridRow(int rowNumber, Control attributeName, Control attributeType)
        {
            var grid = new Grid
            {
                Width = attributeName.Width + attributeType.Width,
                Height = AttributeRowHeight
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(attributeName.Width)});
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(attributeType.Width)});
            grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(AttributeRowHeight)});

            Grid.SetRow(attributeName, 0);
            Grid.SetRow(attributeType, 0);
            Grid.SetColumn(attributeName, 0);
            Grid.SetColumn(attributeType, 1);
            grid.Children.Add(attributeName);
            grid.Children.Add(attributeType);

            Grid.SetRow(grid, rowNumber);
            EntityVisual.Children.Add(grid);
            return grid;
        }
    }
}