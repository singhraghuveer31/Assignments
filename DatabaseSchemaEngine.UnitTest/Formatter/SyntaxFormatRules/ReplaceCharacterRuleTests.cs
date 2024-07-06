using DatabaseSchemaEngine.Formatter.SyntaxFormatRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.UnitTest.Formatter.SyntaxFormatRules
{
    [TestClass]
    public class ReplaceCharacterRuleTests
    {
        private ReplaceCharacterRule CreateReplaceCharacterRule(char oldChar, char newChar)
        {
            return new ReplaceCharacterRule(
                oldChar,
                newChar);
        }

        [TestMethod]
        public void ReplaceSpaceWithUnderScoreTest()
        {
            // Arrange
            var replaceCharacterRule = this.CreateReplaceCharacterRule(' ', '_');
            string syntax = "car name 1";

            // Act
            var result = replaceCharacterRule.Format(
                syntax);

            // Assert
            Assert.AreEqual("car_name_1", result);
        }

        [TestMethod]
        public void ReplaceCommaWithUnderScoreTest()
        {
            // Arrange
            var replaceCharacterRule = this.CreateReplaceCharacterRule(',', '_');
            string syntax = "car,name 1";

            // Act
            var result = replaceCharacterRule.Format(
                syntax);

            // Assert
            Assert.AreEqual("car_name 1", result);
        }
    }
}
