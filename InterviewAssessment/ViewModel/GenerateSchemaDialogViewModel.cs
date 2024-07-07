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
using System.IO;
using DatabaseSchemaEngine.Helper;
using Newtonsoft.Json;
using DomainModelEditor.Model;
using DomainModelEditor.Model.Entities;
using Autofac;
using DomainModelEditor.Util;

namespace DomainModelEditor.ViewModel
{
    /// <summary>
    /// Defines View Model for  GenerateSchemaDialog view.
    /// </summary>
    public class GenerateSchemaDialogViewModel : BaseViewModel<GenerateSchemaDialogViewModel>
    {
        #region Constructor

        public GenerateSchemaDialogViewModel(IDatabaseSchemaGeneratorService databaseSchemaGeneratorService, IGenerateSchemaRepository generateSchemaRepository, IMainWindowViewModel mainWindowViewModel)
        {
            this.databaseSchemaGeneratorService = databaseSchemaGeneratorService;
            this.generateSchemaRepository = generateSchemaRepository;
            this.mainWindowViewModel = mainWindowViewModel;
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
            DomainModelMetadataDbContext = BootStrapper.Resolve<IDomainModelMetadataContext>();
            Initialize();
        }

        #endregion

        #region Fields

        private bool isEntityUpdateRequired;
        private List<EntityDetail> parserEntities;
        private ICommand generateSchemaClickCommand;
        private ObservableCollection<ILookup> targetFrameworks;
        private ILookup selectedTargetFramework;
        private ObservableCollection<CheckListItem> schemaGenerationOptions;
        private ObservableCollection<ILookup> namingConventions;
        private ILookup selectedNamingConvention;
        private readonly IDatabaseSchemaGeneratorService databaseSchemaGeneratorService;
        private readonly IGenerateSchemaRepository generateSchemaRepository;
        private readonly IDatabaseSchemaGenerationRepository databaseSchemaGenerationRepository;
        IDomainModelMetadataContext DomainModelMetadataDbContext;
        private readonly IMainWindowViewModel mainWindowViewModel;

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
                schemaGenerationOptions.Add(new CheckListItem { Lookup = item });
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
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while generateing database schema.", nameof(GenerateSchema));
                ShowFailedMessageBox(string.Empty);
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

        #region Schema Generation Methods
        private void GenerateDatabaseSchema()
        {
            var schemaGenerationInput = PrepareSchemaGenerationInput();

            databaseSchemaGeneratorService.GenerateDatabaseSchema(schemaGenerationInput);
            HandleSchemaGenerationOutput(databaseSchemaGeneratorService.Output);
        }

        private ISchemaGenerationInput PrepareSchemaGenerationInput()
        {
            var entityDetails = GetEntitiesForSchemaGeneration();

            var schemaGenerationOptions = SchemaGenerationOptions.Where(x => x.Checked).Select(x => x.Lookup).ToList();

            if (SelectedNamingConvention != null)
            {
                schemaGenerationOptions.Add(SelectedNamingConvention);
            }

            return new SchemaGenerationInput(GetTargetFramework(), entityDetails,schemaGenerationOptions);
        }

        private List<EntityDetail> GetEntitiesForSchemaGeneration()
        {
            var entityDetails = new List<EntityDetail>();
            try
            {
                if (File.Exists(Configuration.DomainModelMetadataOutputPath))
                {
                    var inputResults = MessageBox.Show("Domain model mapping file already exists. Do you want to use that file for schema generation ?", "Schema Generation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                    if (inputResults == MessageBoxResult.Yes)
                    {
                        isEntityUpdateRequired = true;
                        var input = File.ReadAllText(Configuration.DomainModelMetadataOutputPath);
                        parserEntities = JsonConvert.DeserializeObject<List<EntityDetail>>(input);
                        return parserEntities;
                    }
                }

                foreach (var entity in Entities)
                {
                    var attribures = entity.Attributes.Select(x => new AttributeDetail(x.Id, x.DataType.ToString(), x.Name, entity.Name)).ToList();
                    entityDetails.Add(new EntityDetail(entity.Entity.Id, entity.Entity.Name, attribures));
                }
            }
            catch (Exception ex)
            {
                isEntityUpdateRequired = false;
                throw new Exception("Error while getting entity detail.", ex);
            }

            return entityDetails;
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

        private void HandleSchemaGenerationOutput(ISchemaGenerationOutput output)
        {
            if (!output.IsSuccess)
            {
                string message = string.Empty;
                if (output.ValidationMessages.Any())
                {
                    message = string.Join(Environment.NewLine, output.ValidationMessages);
                }
                ShowFailedMessageBox(message);
            }
            else
            {
                if (Configuration.AllowEntityUpdateFromMetadataFile)
                    SyncEntities();

                MessageBox.Show("Schema file generated", "Schema Generation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private static string ShowFailedMessageBox(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                var filePath = Configuration.LogPath;
                message = $"Schema Generation Failed. Check log file for more details {filePath}";
            }

            MessageBox.Show(message, "Schema Generation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            return message;
        }

        private void SyncEntities()
        {
            try
            {
                if (parserEntities != null && parserEntities.Any())
                {
                    foreach (var entity in parserEntities)
                    {
                        var existingEntity = Entities.Find(x => x.Entity.Id == entity.Id)?.Entity;

                        if (existingEntity == null)
                        {
                            var randomNrGenerator = new Random();
                            mainWindowViewModel.AddEntity(entity.EntityName, 0, 0);
                            existingEntity = mainWindowViewModel.Entities.Find(x => x.Name == entity.EntityName)?.Entity;
                        }

                        if (existingEntity == null)
                            continue;

                        UpdateEntity(entity, existingEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while syncing entitiies", ex);
            }

        }

        private void UpdateEntity(IEntityDetail entity, Model.Entities.Entity existingEntity)
        {
            existingEntity.Name = entity.EntityName;
            if (existingEntity.Attributes == null)
            {
                existingEntity.Attributes = new List<Model.Entities.Attribute>();
            }

            foreach (var attribute in entity.Attributes)
            {
                var existingAttribute = existingEntity.Attributes.FirstOrDefault(x => x.Id == attribute.Id);

                var isSuccess = Enum.TryParse<DataType>(attribute.Type, true, out DataType updatedDataType);

                if (!isSuccess)
                {
                    throw new Exception($"Unable to parse data type {attribute.Type}");
                }

                AddOrUpdateAttibute(existingEntity, attribute, existingAttribute, updatedDataType);
            }
            DomainModelMetadataDbContext.Entities.Update(existingEntity);
            DomainModelMetadataDbContext.SaveChanges();
        }

        private static void AddOrUpdateAttibute(Model.Entities.Entity existingEntity, IAttributeDetail attribute, Model.Entities.Attribute existingAttribute, DataType updatedDataType)
        {
            if (existingAttribute == null)
            {
                existingEntity.Attributes.Add( new Model.Entities.Attribute { Entity = existingEntity, Name = attribute.Name, DataType = updatedDataType });
            }
            else
            {
                existingAttribute.Name = attribute.Name;
                existingAttribute.DataType = updatedDataType;
            }
        }
        #endregion

        #endregion
    }
}
