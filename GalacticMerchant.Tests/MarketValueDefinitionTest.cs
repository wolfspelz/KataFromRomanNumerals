using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class MarketValueDefinitionTest
    {
        [TestMethod]
        public void Parse()
        {
            // Arrange
            var calc = new Calculator();
            calc.LearnRomanDigitAlias("glob", "I");
            var stmt = new Statements.MarketValueDefinition(calc);

            // Act
            stmt.Parse("glob glob Silver is 34 Credits");

            // Assert
            Assert.AreEqual("2 Silver: 34", stmt.ToString());
        }

        [TestMethod]
        public void Execute()
        {
            // Arrange
            var calc = new Calculator();
            var stmt = new Statements.RomanAliasDefinition(calc);
            stmt.Parse( "glob is I");

            // Act
            stmt.Execute();

            // Assert
            Assert.AreEqual("I", calc.GetRomanDigitByAlias("glob"));
        }

        [TestMethod]
        public void ParseFailWithInvalidGalacticRomanSequence()
        {
            // Arrange
            var calc = new Calculator();
            calc.LearnRomanDigitAlias("one", "I");
            calc.LearnRomanDigitAlias("five", "V");
            calc.LearnRomanDigitAlias("ten", "X");
            calc.LearnRomanDigitAlias("fifty", "L");
            var stmt = new Statements.MarketValueDefinition(calc);

            // Act/Assert
            Assert.IsFalse(stmt.Parse("one fifty Silver is 49 Credits"));
        }

    }
}
