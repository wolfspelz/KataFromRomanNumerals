using System.Collections.Generic;
using System.Linq;

namespace GalacticMerchant.Statements
{
    public class MarketValueDefinition : StatementBase, IStatement
    {
        private const string AssignmentToken = "is";
        private const string CurrencyToken = "Credits";

        private string ResourceName { get; set; }
        private int ResourceCount;
        private double TotalValue;

        public MarketValueDefinition(Calculator calculator) : base(calculator) {}

        public bool Parse(string text)
        {
            // Like: glob glob Silver is 34 Credits
            if (string.IsNullOrEmpty(text)) { return Reason("Line empty"); }

            var tokens = Tokenize(text);

            if (tokens.Count < 5) { return Reason("Need at least 5 tokens"); }
            if (!tokens.Contains(AssignmentToken)) { return Reason("Missing '" + AssignmentToken + "'"); }

            var resourceTokens = new List<string>();
            var currencyTokens = new List<string>();
            bool preAssignmentToken = true;
            foreach (var token in tokens) {
                if (token == AssignmentToken) {
                    preAssignmentToken = false;
                } else {
                    if (preAssignmentToken) {
                        resourceTokens.Add(token);
                    } else {
                        currencyTokens.Add(token);
                    }
                }
            }

            if (resourceTokens.Count < 2) { return Reason("Need at least 2 tokens to the left of '" + AssignmentToken + "'"); }
            if (currencyTokens.Count != 2) { return Reason("Need 2 tokens to the right of '" + AssignmentToken + "'"); }
            if (currencyTokens.Last() != CurrencyToken) { return Reason("Missing '" + CurrencyToken + "'"); }

            // Now we got: <resourceTokens> <AssignmentToken> <currencyTokens>

            ResourceName = resourceTokens.Last();
            if (Calculator.IsRomanDigitAlias(ResourceName)) { return Reason("The resource name seems to be a number token"); }

            resourceTokens.Remove(ResourceName);
            string romanNumber = "";
            foreach (var token in resourceTokens) {
                if (!Calculator.IsRomanDigitAlias(token)) { return Reason("This is an unknown number: " + token); }
                romanNumber += Calculator.GetRomanDigitByAlias(token);
            }

            if (!RomanNumber.TryToDecimal(romanNumber, out ResourceCount)) { return Reason("Converting roman number '" + romanNumber + "' to decimal failed"); }
            if (ResourceCount == 0) { return Reason("The resource count is zero"); }

            if (!double.TryParse(currencyTokens.First(), out TotalValue)) { return Reason("The value is not a number"); }

            return true;
        }

        public void Execute()
        {
            Calculator.LearnResourceValue(ResourceName, TotalValue / ResourceCount);
        }

        public override string ToString()
        {
            return ResourceCount + " " + ResourceName + ": " + TotalValue;
        }
    }
}
