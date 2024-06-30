using System.Windows;
using System.Windows.Input;

namespace DomainModelEditor.View.Dialog
{
    /// <summary>
    /// Interaction logic for NamePrompt.xaml
    /// </summary>
    public partial class AddEntityDialog : Window
    {
        public string EntityName => NewEntityName.Text;

        public AddEntityDialog()
        {
            InitializeComponent();

            Loaded += (o,e) => NewEntityName.Focus(); 
        }
        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NewEntityName.Text = "";
            Close();
        }

        private void NewEntityName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(EntityName))
            {
                Close();
            }
        }
    }
}
