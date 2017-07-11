using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class AcceptanceTest
    {
        [TestMethod]
        public void BlobFromProblemDefinition()
        {
            // Arrange
            var inputData =
@"glob is I
prok is V
pish is X
tegj is L
glob glob Silver is 34 Credits
glob prok Gold is 57800 Credits
pish pish Iron is 3910 Credits
how much is pish tegj glob glob ?
how many Credits is glob prok Silver ?
how many Credits is glob prok Gold ?
how many Credits is glob prok Iron ?
how much wood could a woodchuck chuck if a woodchuck could chuck wood ?
";

            var expectedOutput =
@"pish tegj glob glob is 42
glob prok Silver is 68 Credits
glob prok Gold is 57800 Credits
glob prok Iron is 782 Credits
I have no idea what you are talking about
";

            inputData = inputData.Replace("\r\n", "\n");
            expectedOutput = expectedOutput.Replace("\r\n", "\n");

            var calc = new Calculator();

            var outputData = "";
            calc.Log.Levels = "User,Warning,Error";
            calc.Log.Sink = (logLevel, logMessage) => {
                if (logLevel == "User") {
                    outputData += logMessage + "\n";
                }
            };

            var lines = inputData.Split(new[] { '\n' });

            // Act
            foreach (var line in lines) {
                calc.Parse(line).Execute();
            }

            // Assert
            outputData = outputData.Replace("\r\n", "\n");
            Assert.AreEqual(expectedOutput, outputData);
        }
    }
}
