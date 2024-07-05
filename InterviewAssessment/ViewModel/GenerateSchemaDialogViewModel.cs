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
using DatabaseSchemaEngine.Enum;
using System;
using DatabaseSchemaEngine.Lookup;

namespace DomainModelEditor.ViewModel
{
    /// <summary>
    /// Defines View Model for  GenerateSchemaDialog view.
    /// </summary>
    public class GenerateSchemaDialogViewModel : BaseViewModel<GenerateSchemaDialogViewModel>
	{
		#region Constructor

		public GenerateSchemaDialogViewModel(IDatabaseSchemaGeneratorService databaseSchemaGeneratorService, IGenerateSchemaRepository generateSchemaRepository)
		{
            this.databaseSchemaGeneratorService = databaseSchemaGeneratorService;
            this.generateSchemaRepository = generateSchemaRepository;
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
			Initialize();
        }

		#endregion

		#region Fields

		private ICommand generateSchemaClickCommand;
		private ObservableCollection<ILookup> targetFrameworks;
		private ILookup selectedTargetFramework;
        private ObservableCollection<CheckListItem> schemaGenerationOptions;
        private ObservableCollection<ILookup> namingConventions;
        private ILookup selectedNamingConvention;
        private readonly IDatabaseSchemaGeneratorService databaseSchemaGeneratorService;
        private readonly IGenerateSchemaRepository generateSchemaRepository;
        private readonly IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;

        #endregion

        #region Properties

        public List<IEntityViewModel> Entities { get; set; }

        public ObservableCollection<ILookup> TargetFrameworks { get { return targetFrameworks; } }

        public ObservableCollection<CheckListItem> SchemaGenerationOptions { get { return schemaGenerationOptions; } }

        public ObservableCollection<ILookup> NamingConventions { get { return namingConventions; } }

        public ILookup SelectedTargetFramework
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

        public ILookup SelectedNamingConvention
        {
            get
            {
                return selectedNamingConvention;
            }
            set
            {
                selectedNamingConvention = value;
                NotifyPropertyChanged(nameof(SelectedNamingConvention));
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
            LoadSchemaGenerationOptions();
            LoadNamingConventions();
		}

		private void LoadTargetFrameworks()
		{
			targetFrameworks = new ObservableCollection<ILookup>();
			foreach (var item in generateSchemaRepository.GetTargetFrameworks())
			{
				targetFrameworks.Add(item);
			};
		}

        private void LoadSchemaGenerationOptions()
        {
            schemaGenerationOptions = new ObservableCollection<CheckListItem>();
            foreach (var item in generateSchemaRepository.GetSchemaGenerationOptions())
            {
                schemaGenerationOptions.Add(new CheckListItem {Lookup = item } );
            };
        }

        private void LoadNamingConventions()
        {
            namingConventions = new ObservableCollection<ILookup>();
            foreach (var item in generateSchemaRepository.GetNamingConventions())
            {
                namingConventions.Add(item);
            };
        }

        private void GenerateSchema(Window win)
		{
            try
            {
                if (!IsValidated())
                {
                    return;
                }
                GenerateDatabaseSchema();
                CloseWindow(win);
            }
            catch (Exception ex) 
            {
                Logger.Error("Error occurred while generateing database schema.", ex);
            }
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
            var schemaGenerationInput = PrepareSchemaGenerationInput();

            databaseSchemaGeneratorService.GenerateDatabaseSchema(schemaGenerationInput);
        }

        private ISchemaGenerationInput PrepareSchemaGenerationInput() 
        {
            var entityDetails = new List<IEntityDetail>();

            foreach (var entity in Entities)
            {
                var attribures = entity.Attributes.Select(x => new AttributeDetail(x.DataType.ToString(), x.Name, entity.Name)).ToList<IAttributeDetail>();
                entityDetails.Add(new EntityDetail(entity.Name, attribures));
            }

            var schemaGenerationOptions = SchemaGenerationOptions.Where(x => x.Checked).Select(x => x.Lookup);

            return new SchemaGenerationInput
            {
                TargetFramework = GetTargetFramework(),
                EntityDetails = entityDetails,
                SchemaGenerationOptions = schemaGenerationOptions
            };
        }

        private TargetDatabaseFrameworkValues GetTargetFramework() 
        {
            try
            {
                Enum.TryParse(SelectedTargetFramework.Code, out TargetDatabaseFrameworkValues result);
                return result;
            }
            catch
            {
                throw new Exception($"Database framework: {SelectedTargetFramework.Name} not supported"); 
            }
        }

        #endregion
    }
}
