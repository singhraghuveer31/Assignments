using System.Windows.Controls;
using System.Windows.Input;
using DomainModelEditor.Model.Entities;

namespace DomainModelEditor.UserControl
{
    /// <summary>
    /// This class is used to replace the textbox where a new attribute name can be entered with a label
    /// after saving the entered text as a new attribute.
    /// 
    /// The PerformAndDispose action should be connected to the KeyDown eventhandler of the textbox to be
    /// replaced. Once replaced, the references to the textbox are removed to allow for garbage collection.
    /// </summary>
    class EntityAttributeSaveAction
    {
        private Attribute Attribute { get; set; }
        public int RowNumber { get; set; }
        public Entity Entity { get; set; }
        public TextBox NewAttributeNameTextBox { get; set; }
        public ComboBox NewAttributeTypeDropDown { get; set; }
        public Grid NewAttributeGrid { get; set; }

        public void PerformAndDispose(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrWhiteSpace(NewAttributeNameTextBox.Text))
                    return;

                Attribute = Entity.DataContext.AddNewAttribute(NewAttributeNameTextBox.Text,
                    (DataType) NewAttributeTypeDropDown.SelectedValue);

                ReplaceTextBoxWithLabel();

                FocusNextRelevantElement();

                Dispose();
            }
        }

        /// <summary>
        /// The goal is to select the first textbox after the one that has just been saved, or 
        /// the first one from the top if any.
        /// </summary>
        private void FocusNextRelevantElement()
        {
            Grid firstGrid = null;
            Grid nextGrid = null;

            foreach (var element in Entity.EntityVisual.Children)
            {
                if (element is Grid grid && grid.Children.Count == 2)
                {
                    var gridRowNumber = Grid.GetRow(grid);

                    if (gridRowNumber > RowNumber &&
                        (nextGrid == null || Grid.GetRow(nextGrid) > gridRowNumber))
                    {
                        nextGrid = grid;
                        continue;
                    }

                    if (gridRowNumber <= RowNumber &&
                        (firstGrid == null || Grid.GetRow(firstGrid) > gridRowNumber))
                    {
                        firstGrid = grid;
                    }
                }
            }

            var gridToFocusOn = nextGrid ?? firstGrid;
            gridToFocusOn?.Children[0]?.Focus();
        }

        private void Dispose()
        {
            // Enable garbage collection of the no longer used TextBox;
            NewAttributeNameTextBox.KeyDown -= PerformAndDispose;
            NewAttributeNameTextBox = null;

            NewAttributeTypeDropDown.KeyDown -= PerformAndDispose;
            NewAttributeTypeDropDown = null;

            NewAttributeGrid = null;
        }

        private void ReplaceTextBoxWithLabel()
        {
            var (nameLabel, typeLabel) = Entity.CreateAttributeControls(Attribute);

            Entity.EntityVisual.Children.Remove(NewAttributeGrid);

            Entity.SetGridRow(RowNumber, nameLabel, typeLabel);
        }
    }
}