using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Formatter.SyntaxFormatRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.UnitTest.Formatter.SyntaxFormatRules
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
            var namingConventionRule = this.CreateNamingConventionRule(NamingConventionValues.CapitalCaseFirst);
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
            var namingConventionRule = this.CreateNamingConventionRule(NamingConventionValues.CamelCase);
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
            var namingConventionRule = this.CreateNamingConventionRule(NamingConventionValues.LowerCase);
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
            var namingConventionRule = this.CreateNamingConventionRule(NamingConventionValues.UpperCase);
            string syntax = "CarName";

            // Act
            var result = namingConventionRule.Format(syntax);

            // Assert
            Assert.AreEqual("CARNAME", result);
        }
    }
}
