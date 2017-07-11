using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class MarketValueQuery
    {
        [TestMethod]
        public void Parse()
        {
            // Arrange
            var calc = new Calculator();
            calc.LearnRomanDigitAlias("prok", "V");
            calc.LearnRomanDigitAlias("glob", "I");
            calc.LearnResourceValue("Silver", 17);
            var stmt = new Statements.MarketValueQuery(calc);

            // Act
            stmt.Parse("how many Credits is glob prok Silver ?");

            // Assert
            Assert.AreEqual("4 Silver", stmt.ToString());
        }

        [TestMethod]
        public void Execute()
        {
            // Arrange
            var calc = new Calculator();
            calc.LearnRomanDigitAlias("prok", "V");
            calc.LearnRomanDigitAlias("glob", "I");
            calc.LearnResourceValue("Silver", 17);
            var stmt = new Statements.MarketValueQuery(calc);
            stmt.Parse("how many Credits is glob prok Silver ?");

            // Act
            stmt.Execute();

            // Assert
            Assert.AreEqual(68, stmt.GetTotalCredits());
        }
    }
}
