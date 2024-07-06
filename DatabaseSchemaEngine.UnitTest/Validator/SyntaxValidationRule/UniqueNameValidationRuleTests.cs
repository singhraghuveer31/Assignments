using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Helper.Cache;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.UnitTest.Validator.SyntaxValidationRule
{
    [TestClass]
    public class UniqueNameValidationRuleTests
    {
        [TestInitialize]
        public void TestInitialize()
        { 
            SchemaMappingCache.EntityNameValidationCache.Clear();
        }

        private UniqueNameValidationRule CreateUniqueNameValidationRule(TypeNameValues typeName)
        {
            return new UniqueNameValidationRule(typeName);
        }

        [TestMethod]
        public void UniqueEntityNameSucessTest()
        {
            // Arrange
            var uniqueNameValidationRule = this.CreateUniqueNameValidationRule(TypeNameValues.Entity);
            string name = "CarTest";

            // Act
            uniqueNameValidationRule.IsValid(name);

            name = "CarTest2";
            var result = uniqueNameValidationRule.IsValid(name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UniqueEntityNameFailTest()
        {
            // Arrange
            var uniqueNameValidationRule = this.CreateUniqueNameValidationRule(TypeNameValues.Entity);
            string name = "CarTest";

            // Act
            uniqueNameValidationRule.IsValid(name);

            var result = uniqueNameValidationRule.IsValid(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UniqueAttributeNameSucessTest()
        {
            // Arrange
            var uniqueNameValidationRule = this.CreateUniqueNameValidationRule(TypeNameValues.Attibute);
            string name = "CarTest";

            // Act
            uniqueNameValidationRule.IsValid(name);

            name = "CarTest2";
            var result = uniqueNameValidationRule.IsValid(name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UniqueAttributeNameFailTest()
        {
            // Arrange
            var uniqueNameValidationRule = this.CreateUniqueNameValidationRule(TypeNameValues.Attibute);
            string name = "CarTest";

            // Act
            uniqueNameValidationRule.IsValid(name);

            var result = uniqueNameValidationRule.IsValid(name);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
