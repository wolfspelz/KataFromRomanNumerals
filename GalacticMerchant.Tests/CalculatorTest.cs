using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void LearnRomanDigit()
        {
            // Arrange
            var calc = new Calculator();

            // Act
            calc.LearnRomanDigitAlias("foo", "I");
            calc.LearnRomanDigitAlias("bar", "V");

            // Assert
            Assert.IsTrue(calc.IsRomanDigitAlias("foo"));
            Assert.AreEqual("V", calc.GetRomanDigitByAlias("bar"));
        }

        [TestMethod]
        public void LearnResourceValue()
        {
            // Arrange
            var calc = new Calculator();

            // Act
            calc.LearnResourceValue("Pi", 3.1415);

            // Assert
            Assert.IsTrue(calc.IsResource("Pi"));
            Assert.AreEqual(3.1415, calc.GetResourceValueInCredits("Pi"));
        }
    }
}
