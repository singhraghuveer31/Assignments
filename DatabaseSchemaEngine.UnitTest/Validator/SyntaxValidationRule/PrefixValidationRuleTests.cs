using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.Test.Validator.SyntaxValidationRule
{
    [TestClass]
    public class PrefixValidationRuleTests
    {

        private PrefixValidationRule CreatePrefixValidationRule(PrefixConventionValues prefixConvention)
        {
            return new PrefixValidationRule(
                prefixConvention);
        }

        [TestMethod]
        public void StartsWithLetterSuccessTest()
        {
            // Arrange
            var prefixValidationRule = CreatePrefixValidationRule(PrefixConventionValues.Letter);
            string syntax = "CarName";

            // Act
            var result = prefixValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StartsWithLetterFailTest()
        {
            // Arrange
            var prefixValidationRule = CreatePrefixValidationRule(PrefixConventionValues.Letter);
            string syntax = "1CarName";

            // Act
            var result = prefixValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StartsWithUnderScoreSuccessTest()
        {
            // Arrange
            var prefixValidationRule = CreatePrefixValidationRule(PrefixConventionValues.UnderScore);
            string syntax = "_CarName";

            // Act
            var result = prefixValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StartsWithUnderScoreFailTest()
        {
            // Arrange
            var prefixValidationRule = CreatePrefixValidationRule(PrefixConventionValues.UnderScore);
            string syntax = "CarName";

            // Act
            var result = prefixValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
