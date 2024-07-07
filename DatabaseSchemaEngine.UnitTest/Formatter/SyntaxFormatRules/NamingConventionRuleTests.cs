using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Formatter.SyntaxFormatRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.Test.Formatter.SyntaxFormatRules
{
    [TestClass]
    public class NamingConventionRuleTests
    {
        private NamingConventionRule CreateNamingConventionRule(NamingConventionValues namingConvention)
        {
            return new NamingConventionRule(namingConvention);
        }

        [TestMethod]
        public void CapitalCaseFirstTest()
        {
            // Arrange
            var namingConventionRule = CreateNamingConventionRule(NamingConventionValues.CapitalCaseFirst);
            string syntax = "carname";

            // Act
            var result = namingConventionRule.Format(syntax);

            // Assert
            Assert.AreEqual("Carname", result);
        }

        [TestMethod]
        public void CamelCaseTest()
        {
            // Arrange
            var namingConventionRule = CreateNamingConventionRule(NamingConventionValues.CamelCase);
            string syntax = "car name";

            // Act
            var result = namingConventionRule.Format(syntax);

            // Assert
            Assert.AreEqual("carName", result);
        }

        [TestMethod]
        public void AllLowerCaseTest()
        {
            // Arrange
            var namingConventionRule = CreateNamingConventionRule(NamingConventionValues.LowerCase);
            string syntax = "CarName";

            // Act
            var result = namingConventionRule.Format(syntax);

            // Assert
            Assert.AreEqual("carname", result);
        }

        [TestMethod]
        public void AllUperCaseTest()
        {
            // Arrange
            var namingConventionRule = CreateNamingConventionRule(NamingConventionValues.UpperCase);
            string syntax = "CarName";

            // Act
            var result = namingConventionRule.Format(syntax);

            // Assert
            Assert.AreEqual("CARNAME", result);
        }
    }
}
