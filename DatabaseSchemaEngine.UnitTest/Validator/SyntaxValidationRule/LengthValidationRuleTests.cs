using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.Test.Validator.SyntaxValidationRule
{
    [TestClass]
    public class LengthValidationRuleTests
    {
        private LengthValidationRule CreateLengthValidationRule(int minLength, int maxLength)
        {
            return new LengthValidationRule(
                minLength,
                maxLength);
        }

        [TestMethod]
        public void FixLengthValiadtionSuccessTest()
        {
            // Arrange
            var lengthValidationRule = CreateLengthValidationRule(8, 8);
            string syntax = "Carname1";

            // Act
            var result = lengthValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FixLengthValiadtionFailTest()
        {
            // Arrange
            var lengthValidationRule = CreateLengthValidationRule(8, 8);
            string syntax = "Carname12";

            // Act
            var result = lengthValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VariableLengthValiadtionSuccessTest()
        {
            // Arrange
            var lengthValidationRule = CreateLengthValidationRule(8, 10);
            string syntax = "Carname12";

            // Act
            var result = lengthValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VariableLengthValiadtionFailTest()
        {
            // Arrange
            var lengthValidationRule = CreateLengthValidationRule(8, 10);
            string syntax = "Carname";

            // Act
            var result = lengthValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
