using DatabaseSchemaEngine.Constants;
using DatabaseSchemaEngine.Enum;
using DatabaseSchemaEngine.Validator.SyntaxValidationRule;
using DatabaseSchemaEngine.Validator.ValidationMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSchemaEngine.UnitTest.Validator.ValidationMessage
{
    [TestClass]
    public class SFCDBValidationMessageProviderTests
    {

        private SFCDBValidationMessageProvider CreateProvider()
        {
            return new SFCDBValidationMessageProvider();
        }

        [TestMethod]
        public void GetLengthValidationMessageTest()
        {
            // Arrange
            var dataStoreName = "Test";
            var expectedMessage = string.Format(ValidationConstant.TableNameLengthValidation, "data store", dataStoreName, ValidationConstant.SFCDBDataStoreLength);
            var provider = this.CreateProvider();

            // Act
            var result = provider.GetValidationMessage(new LengthValidationRule(2, 3),dataStoreName, "");

            // Assert
            Assert.AreEqual(expectedMessage, result);

            // Arrange
            var propName = "Test Prop";
            expectedMessage = string.Format(ValidationConstant.ColumnLengthValidationMessage, "prop", propName, "data store", dataStoreName, ValidationConstant.SFCDBPropMinLength, ValidationConstant.SFCDBPropMaxLength);


            // Act
            result = provider.GetValidationMessage(new LengthValidationRule(2, 3), dataStoreName, propName);

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }


        [TestMethod]
        public void GetNameAlreadyExistsValidationMessageTest()
        {
            // Arrange
            var dataStoreName = "Test";
            var expectedMessage = string.Format(ValidationConstant.TableNameAlreadyExistsValidationMessage, "Data store", dataStoreName);
            var provider = this.CreateProvider();

            // Act
            var result = provider.GetValidationMessage(new UniqueNameValidationRule(TypeNameValues.Entity), dataStoreName, "");

            // Assert
            Assert.AreEqual(expectedMessage, result);

            // Arrange
            var propName = "Test Prop";
            expectedMessage = string.Format(ValidationConstant.ColumnNameAlreadyExistsValidationMessage, "Prop", propName, "data store", dataStoreName);


            // Act
            result = provider.GetValidationMessage(new UniqueNameValidationRule(TypeNameValues.Attibute), dataStoreName, propName);

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }

        [TestMethod]
        public void GetPrefixValidationMessageForPropTest()
        {
            // Arrange
            var dataStoreName = "Test";
            var prefixConvention = PrefixConventionValues.Letter;
            var expectedMessage = string.Format(ValidationConstant.TableNamePrefixValidationMessage, "Data store", dataStoreName, prefixConvention.ToString());
            var provider = this.CreateProvider();

            // Act
            var result = provider.GetValidationMessage(new PrefixValidationRule(prefixConvention), dataStoreName, "");

            // Assert
            Assert.AreEqual(expectedMessage, result);

            // Arrange
            var propName = "Test Prop";
            expectedMessage = string.Format(ValidationConstant.ColumnPrefixValidationMessage, "Prop", propName, "data store", dataStoreName, prefixConvention.ToString());


            // Act
            result = provider.GetValidationMessage(new PrefixValidationRule(prefixConvention), dataStoreName, propName);

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }


        [TestMethod]
        public void GetSpecialCharValidationMessageTest()
        {
            // Arrange
            var allowedSpecialChars = ValidationConstant.SFCDBAllowedSpecialChars;
            var dataStoreName = "Test";
            var expectedMessage = string.Format(ValidationConstant.TableNameSpecialCharValidationMessage, "data store", dataStoreName, string.Join(",", allowedSpecialChars));
            var provider = this.CreateProvider();

            // Act
            var result = provider.GetValidationMessage(new SpecialCharacterValidationRule(allowedSpecialChars), dataStoreName, "");

            // Assert
            Assert.AreEqual(expectedMessage, result);

            // Arrange
            var propName = "Test Prop";
            expectedMessage = string.Format(ValidationConstant.ColumnSpecialCharValidationMessage, "prop", propName, "data store", dataStoreName, string.Join(",", allowedSpecialChars));


            // Act
            result = provider.GetValidationMessage(new SpecialCharacterValidationRule(allowedSpecialChars), dataStoreName, propName);

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }
    }
}
