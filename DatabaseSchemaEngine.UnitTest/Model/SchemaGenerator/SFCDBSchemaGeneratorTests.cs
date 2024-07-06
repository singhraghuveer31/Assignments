using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Helper;
using DatabaseSchemaEngine.Model.EntityDetail;
using DatabaseSchemaEngine.Model.SchemaGenerator;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DatabaseSchemaEngine.UnitTest.Model.SchemaGenerator
{
    [TestClass]
    public class SFCDBSchemaGeneratorTests : SchemaGenerationTestBase
    {
        private MockRepository mockRepository;
        private Mock<ISchemaMapper> mockSchemaMapper;
        string outPutDirectory = "C:\\MockTest";

        [TestInitialize]
        public void TestInitialize()
        {
            SetupSFCDBConfig(outPutDirectory);
            CreateSFCDBOutputDirectory(outPutDirectory);
            EmptySFCDBOutputDirectory(outPutDirectory);
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSchemaMapper = this.mockRepository.Create<ISchemaMapper>();
            this.mockSchemaMapper.Setup(x => x.GetSchemaMappings()).Returns(GetMappings());
        }

        private SFCDBSchemaGenerator CreateSFCDBSchemaGenerator()
        {
            return new SFCDBSchemaGenerator(
                this.mockSchemaMapper.Object);
        }

        [TestMethod]
        public void SchemaGenerationSuccessfulTest()
        {
            // Arrange
            var sFCDBSchemaGenerator = this.CreateSFCDBSchemaGenerator();
            var entities = GetEntityDetails("String","Attribute1","Attribute2","Test");

            // Act
            sFCDBSchemaGenerator.GenerateDatabaseSchema(entities);

            // Assert
            Assert.IsTrue(Directory.GetFiles(outPutDirectory).Count() == 1);
        }

        [TestMethod]
        public void SchemaGenerationWhenNoMappingTest()
        {
            // Arrange
            this.mockSchemaMapper.Setup(x => x.GetSchemaMappings()).Returns((SchemaMappingDetail)null);
            var sFCDBSchemaGenerator = this.CreateSFCDBSchemaGenerator();
            var entities = GetEntityDetails("String", "Attribute1", "Attribute2", "Test");

            // Act
             sFCDBSchemaGenerator.GenerateDatabaseSchema(entities);

            // Assert
            Assert.IsTrue(Directory.GetFiles(outPutDirectory).Count() == 0);

        }

        [TestMethod]
        public void SchemaGenerationWhenMappingWithNoTypeTest()
        {
            // Arrange
            var type = "bool";
            var prop1 = "Attribute1";
            var prop2 = "Attribute2";
            var entityName = "Test";

            var sFCDBSchemaGenerator = this.CreateSFCDBSchemaGenerator();
            var entities = GetEntityDetails(type, prop1, prop2, entityName);

            // Act
            var exception = Assert.ThrowsException<Exception>(() => sFCDBSchemaGenerator.GenerateDatabaseSchema(entities));

            // Assert
            var expectedMessage = string.Format(Common.DataTypeMappingMissing, type, prop1, entityName);

            Assert.IsTrue(exception.InnerException.Message.Contains(expectedMessage));
            Assert.IsTrue(Directory.GetFiles(outPutDirectory).Count() == 0);
        }

        private SchemaMappingDetail GetMappings() 
        {
            var sfcdbTypeMap = new Map<string, string>();
            sfcdbTypeMap.Add("String", "unlimited_text");
            sfcdbTypeMap.Add("Int16", "small_numerical");
            sfcdbTypeMap.Add("Int", "numerical");

            var schemaMappingDetails = new SchemaMappingDetail(sfcdbTypeMap, SFCDBSchemaGeneratorConstant.DataStoreTemplateFileName, SFCDBSchemaGeneratorConstant.PropTemplateFileName, "mdl", "cs");

            return schemaMappingDetails;
        }

        private SchemaMappingDetail GetEmptyMappingObject()
        {
            var sfcdbTypeMap = new Map<string, string>();

            var schemaMappingDetails = new SchemaMappingDetail(sfcdbTypeMap, SFCDBSchemaGeneratorConstant.DataStoreTemplateFileName, SFCDBSchemaGeneratorConstant.PropTemplateFileName, "mdl", "cs");

            return schemaMappingDetails;
        }

        private List<IEntityDetail> GetEntityDetails(string type, string prop1Name, string prop2Name, string entityName)
        {
            var entityDetails = new List<IEntityDetail>();

            var attributes = new List<AttributeDetail>
                {
                    new AttributeDetail(1,type, prop1Name, entityName),
                    new AttributeDetail(2,type, prop2Name, entityName)
                };
            entityDetails.Add(new EntityDetail(1, entityName, attributes));
            return entityDetails;
        }
    }
}
