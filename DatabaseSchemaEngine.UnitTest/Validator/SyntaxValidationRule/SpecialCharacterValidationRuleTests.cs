using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.UnitTest.Validator.SyntaxValidationRule
{
    [TestClass]
    public class SpecialCharacterValidationRuleTests
    {
        List<char> charList;
        SpecialCharacterValidationRule specialCharacterValidationRule;

        [TestInitialize]
        public void TestInitialize()
        {
            charList = new List<char> { '_', '-' };
            specialCharacterValidationRule = this.CreateSpecialCharacterValidationRule(charList);
        }

        private SpecialCharacterValidationRule CreateSpecialCharacterValidationRule(List<char> chars)
        {
            return new SpecialCharacterValidationRule(chars);
        }

        [TestMethod]
        public void SpecialCharacterSuccessTest()
        {
            // Arrange
            string syntax = "car_text";

            // Act
            var result = specialCharacterValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SpecialCharacterFailTest()
        {
            // Arrange
            string syntax = "car@text";

            // Act
            var result = specialCharacterValidationRule.IsValid(
                syntax);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
