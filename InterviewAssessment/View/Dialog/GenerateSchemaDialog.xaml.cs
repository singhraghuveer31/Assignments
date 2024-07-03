using DatabaseSchemaEngine.Service.SchemaGeneration;
using System.Windows;

namespace DomainModelEditor.View.Dialog
{
	/// <summary>
	/// Interaction logic for NamePrompt.xaml
	/// </summary>
	public partial class GenerateSchemaDialog : Window
	{
		public GenerateSchemaDialog(IDatabaseSchemaGeneratorService databaseSchemaGeneratorService)
		{
			InitializeComponent();
			this.DataContext = new ViewModel.GenerateSchemaDialogViewModel(databaseSchemaGeneratorService);
		}
	}
}