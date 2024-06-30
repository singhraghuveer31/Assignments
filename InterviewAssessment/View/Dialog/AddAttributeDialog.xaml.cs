using System.Windows;
using System.Windows.Input;

namespace DomainModelEditor.View.Dialog
{
    /// <summary>
    /// Interaction logic for AddAttributeDialog.xaml
    /// </summary>
    public partial class AddAttributeDialog : Window
    {
        public string AttributeName => NewAttributeName.Text;

        public AddAttributeDialog()
        {
            InitializeComponent();
            Loaded += (o, e) => NewAttributeName.Focus();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NewAttributeName.Text = "";
            Close();
        }
        
        private void NewAttributeName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter && !string.IsNullOrWhiteSpace(AttributeName))
            {
                Close();
            }
        }
    }
}
