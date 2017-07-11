using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class RomanNumberQuery
    {
        [TestMethod]
        public void Parse()
        {
            // Arrange
            var calc = new Calculator();
            calc.LearnRomanDigitAlias("pish", "X");
            calc.LearnRomanDigitAlias("tegj", "L");
            calc.LearnRomanDigitAlias("glob", "I");
            var stmt = new Statements.RomanNumberQuery(calc);

            // Act
            stmt.Parse("how much is pish tegj glob glob ?");

            // Assert
            Assert.AreEqual("pish tegj glob glob = 42", stmt.ToString());
        }
    }
}
