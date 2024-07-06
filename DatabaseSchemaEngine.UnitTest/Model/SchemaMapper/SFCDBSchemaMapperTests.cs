using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Helper;
using DatabaseSchemaEngine.Model.SchemaMapper;
using DatabaseSchemaEngine.Model.SchemaMappingDetail;
using DatabaseSchemaEngine.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DatabaseSchemaEngine.UnitTest.Model.SchemaMapper
{
    [TestClass]
    public class SFCDBSchemaMapperTests
    {
        private MockRepository mockRepository;

        private Mock<IDatabaseSchemaGenerationRepository> mockDatabaseSchemaGenerationRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDatabaseSchemaGenerationRepository = this.mockRepository.Create<IDatabaseSchemaGenerationRepository>();
            this.mockDatabaseSchemaGenerationRepository.Setup(x => x.GetSchemaMappings()).Returns(GetSchemaMappings(Enum.TargetDatabaseFrameworkValues.SFCDB.ToString()));
        }

        private SFCDBSchemaMapper CreateSFCDBSchemaMapper()
        {
            return new SFCDBSchemaMapper(
                this.mockDatabaseSchemaGenerationRepository.Object);
        }

        [TestMethod]
        public void GetSchemaMappingsReturnsNullTest()
        {
            // Arrange
            this.mockDatabaseSchemaGenerationRepository.Setup(x => x.GetSchemaMappings()).Returns((Dictionary<string, SchemaMappingDetail>)null);
            var sFCDBSchemaMapper = this.CreateSFCDBSchemaMapper();

            // Act
            var exception = Assert.ThrowsException<Exception>(() => sFCDBSchemaMapper.GetSchemaMappings());

            // Assert
            Assert.IsTrue(exception.Message.Contains(Common.MappingDetailsNotFound));
        }

        [TestMethod]
        public void GetSchemaMappingsDoesNotReturnsForTargetFrameworkTest()
        {
            // Arrange
            this.mockDatabaseSchemaGenerationRepository.Setup(x => x.GetSchemaMappings()).Returns(GetSchemaMappings("Test"));

            var sFCDBSchemaMapper = this.CreateSFCDBSchemaMapper();

            // Act
            var exception = Assert.ThrowsException<Exception>(()=> sFCDBSchemaMapper.GetSchemaMappings());

            // Assert
            Assert.IsTrue(exception.Message.Contains(Common.MappingDetailsNotFound));
        }

        private Dictionary<string, SchemaMappingDetail> GetSchemaMappings(string targetFramework)
        {
            var results = new Dictionary<string, SchemaMappingDetail>();

            var sfcdbTypeMap = new Map<string, string>();
            sfcdbTypeMap.Add("String", "unlimited_text");
            sfcdbTypeMap.Add("Int16", "small_numerical");
            sfcdbTypeMap.Add("Int", "numerical");

            var schemaMappingDetails = new SchemaMappingDetail(sfcdbTypeMap, SFCDBSchemaGeneratorConstant.DataStoreTemplateFileName, SFCDBSchemaGeneratorConstant.PropTemplateFileName, "mdl", "cs");
            results.Add(targetFramework, schemaMappingDetails);

            return results;
        }
    }
}
