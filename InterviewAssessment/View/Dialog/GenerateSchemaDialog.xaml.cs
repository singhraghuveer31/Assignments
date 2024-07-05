using DatabaseSchemaEngine.Service.SchemaGeneration;
using DomainModelEditor.Repository;
using System.Windows;

namespace DomainModelEditor.View.Dialog
{
	/// <summary>
	/// Interaction logic for NamePrompt.xaml
	/// </summary>
	public partial class GenerateSchemaDialog : Window
	{
		public GenerateSchemaDialog(IDatabaseSchemaGeneratorService databaseSchemaGeneratorService, IGenerateSchemaRepository generateSchemaRepository)
		{
			InitializeComponent();
			this.DataContext = new ViewModel.GenerateSchemaDialogViewModel(databaseSchemaGeneratorService, generateSchemaRepository);
		}
    }
}