using System.Windows;

namespace DomainModelEditor.View.Dialog
{
    /// <summary>
    /// Interaction logic for NamePrompt.xaml
    /// </summary>
    public partial class GenerateSchemaDialog : Window
    {
        public bool Continue { get; set; }

        public GenerateSchemaDialog()
        {
            InitializeComponent();
        }


        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            Continue = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}