using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Service.SchemaGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;

namespace DatabaseSchemaEngine.Test.Service.SchemaGeneration
{
    using DatabaseSchemaEngine.Lookup;
    using DatabaseSchemaEngine.Test;
    using System.Collections.Generic;

    [TestClass]
    public class DatabaseSchemaGenerationServiceTests : SchemaGenerationTestBase
    {
        private MockRepository mockRepository;
        private Mock<ILogger> mockLogger;
        string outPutDirectory = "C:\\MockTest";


        public DatabaseSchemaGenerationServiceTests()
        {
            SetupSFCDBConfig(outPutDirectory);
            CreateSFCDBOutputDirectory(outPutDirectory);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            EmptySFCDBOutputDirectory(outPutDirectory);
            mockRepository = new MockRepository(MockBehavior.Strict);

            mockLogger = mockRepository.Create<ILogger>();
            mockLogger.Setup(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        private DatabaseSchemaGenerationService CreateService()
        {
            return new DatabaseSchemaGenerationService(
                mockLogger.Object);
        }

        [TestMethod]
        public void GenerateDatabaseSchemaSuccessTest()
        {
            // Arrange
            var validEntityName = "TestCar1";
            var service = CreateService();
            ISchemaGenerationInput schemaGenerationInput = GetInput(validEntityName, "Int");

            // Act
            service.GenerateDatabaseSchema(schemaGenerationInput);

            // Assert
            Assert.IsTrue(service.Output.IsSuccess);
        }

        [TestMethod]
        public void GenerateDatabaseSchemaWhenValidationErrorTest()
        {
            // Arrange
            var inValidEntityName = "TestCar1233";
            var service = CreateService();
            ISchemaGenerationInput schemaGenerationInput = GetInput(inValidEntityName, "Int");

            // Act
            service.GenerateDatabaseSchema(schemaGenerationInput);

            // Assert
            Assert.IsFalse(service.Output.IsSuccess);
            Assert.IsTrue(service.Output.ValidationMessages.Count() > 0);
        }

        [TestMethod]
        public void GenerateDatabaseSchemaWhenExceptionTest()
        {
            // Arrange
            var validEntityName = "TestCar1";
            var service = CreateService();
            ISchemaGenerationInput schemaGenerationInput = GetInput(validEntityName, "bool");

            // Act
            service.GenerateDatabaseSchema(schemaGenerationInput);

            // Assert
            Assert.IsFalse(service.Output.IsSuccess);
            Assert.IsTrue(mockLogger.Invocations.Count() == 1);
        }

        private ISchemaGenerationInput GetInput(string entityName, string dataType)
        {
            return new SchemaGenerationInput(Enum.TargetDatabaseFrameworkValues.SFCDB, GetEntityDetails(entityName, dataType), GetSchemaGenerationOptions());
        }

        private IEnumerable<Lookup> GetSchemaGenerationOptions()
        {
            var list = new List<Lookup>();
            list.Add(new Lookup { Name = "Test", Code = "Test", TypeName = "Test" });
            return list;
        }

        private List<IEntityDetail> GetEntityDetails(string entityName, string dataType)
        {
            var entityDetails = new List<IEntityDetail>();

            var attributes = new List<AttributeDetail>
                {
                    new AttributeDetail(1,"String", "prop1Name", "entityName"),
                    new AttributeDetail(2,dataType, "prop2Name", "entityName")
                };
            entityDetails.Add(new EntityDetail(1, entityName, attributes));
            return entityDetails;
        }
    }
}
