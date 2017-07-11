using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class RomanAliasDefinitionTest
    {
        [TestMethod]
        public void Parse()
        {
            // Arrange
            var stmt = new Statements.RomanAliasDefinition(new Calculator());

            // Act
            stmt.Parse( "glob is I");

            // Assert
            Assert.AreEqual("glob = I", stmt.ToString());
        }

        [TestMethod]
        public void Execute()
        {
            // Arrange
            var calc = new Calculator();
            var stmt = new Statements.RomanAliasDefinition(calc);
            stmt.Parse("glob is I");

            // Act
            stmt.Execute();

            // Assert
            Assert.AreEqual("I", calc.GetRomanDigitByAlias("glob"));
        }
    }
}
