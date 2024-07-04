using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using DomainModelEditor.Repository;
using System.Collections.ObjectModel;
using DatabaseSchemaEngine.Service.SchemaGeneration;
using DatabaseSchemaEngine.Repository;
using DomainModelEditor.UserControl;
using System.Collections.Generic;
using DatabaseSchemaEngine.Model.EntityDetail;
using System.Linq;

namespace DomainModelEditor.ViewModel
{
	/// <summary>
	/// Defines View Model for  GenerateSchemaDialog view.
	/// </summary>
	public class GenerateSchemaDialogViewModel : BaseViewModel<GenerateSchemaDialogViewModel>
	{
		#region Constructor

		public GenerateSchemaDialogViewModel(IDatabaseSchemaGeneratorService databaseSchemaGeneratorService)
		{
			this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
			Initialize();
			this.databaseSchemaGeneratorService = databaseSchemaGeneratorService;
		}

		#endregion

		#region Fields

		private ICommand generateSchemaClickCommand;
		private ObservableCollection<string> targetFrameworks;
		private string selectedTargetFramework;
		private readonly IDatabaseSchemaGeneratorService databaseSchemaGeneratorService;
		private readonly IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;

        #endregion

        #region Properties

        public List<IEntityViewModel> Entities { get; set; }

        public ObservableCollection<string> TargetFrameworks { get { return targetFrameworks; } }

		public string SelectedTargetFramework
		{
			get
			{
				return selectedTargetFramework;
			}
			set
			{
				selectedTargetFramework = value;
				NotifyPropertyChanged(nameof(SelectedTargetFramework));
			}
		}

		#endregion

		#region Commands

		public RelayCommand<Window> CloseWindowCommand { get; private set; }

		public ICommand GenerateSchemaClickCommand
		{
			get
			{
				if (generateSchemaClickCommand == null)
				{
					generateSchemaClickCommand = new RelayCommand<Window>(GenerateSchema);
				}
				return generateSchemaClickCommand;
			}
		}

		#endregion

		#region Private Methods

		private void Initialize()
		{
			LoadTargetFrameworks();

			if (selectedTargetFramework == null) 
			{
				SelectedTargetFramework = Constants.SchemaGenrationConstant.DEFAULT;
			}
		}

		private void LoadTargetFrameworks()
		{
			targetFrameworks = new ObservableCollection<string>();
			foreach (var item in GenerateSchemaRepository.GetTargetFrameworks())
			{
				targetFrameworks.Add(item);
			};
		}

		private void GenerateSchema(Window win)
		{
			if (!IsValidated()) 
			{
				return;
			}
			GenerateDatabaseSchema();
			CloseWindow(win);
		}

		private void CloseWindow(Window window)
		{
			if (window != null)
			{
				window.Close();
			}
		}

		public bool IsValidated()
		{
			var isValidated = Validations.GenerateSchemaDialogValidator.IsSelectedDataBaseSchemaValid(SelectedTargetFramework);

			if (!isValidated) 
			{
				MessageBox.Show(Constants.ValidationMessage.SelectTargetFramework, "Target Framework", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return isValidated;
		}

		private void GenerateDatabaseSchema() 
		{
			var entityDetails = new List<IEntityDetail>();

			foreach (var entity in Entities) 
			{
				var attribures = entity.Attributes.Select(x=> new AttributeDetail(x.DataType.ToString(), x.Name, entity.Name)).ToList<IAttributeDetail>();
				entityDetails.Add(new EntityDetail(entity.Name, attribures));
			}

            databaseSchemaGeneratorService.GenerateDatabaseSchema(SelectedTargetFramework, entityDetails);
        }

		#endregion
	}
}
