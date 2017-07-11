using System.Linq;

namespace GalacticMerchant.Statements
{
    public class MarketValueQuery : StatementBase, IStatement
    {
        private const string QueryPhrase = "how many Credits is";
        private const string QuerySign = "?";

        private string OriginalNumber = "";
        private string ResourceName = "";
        private int ArabNumber;

        public MarketValueQuery(Calculator calculator) : base(calculator) {}

        public bool Parse(string text)
        {
            // like: how many Credits is glob prok Silver ?
            if (string.IsNullOrEmpty(text)) { return Reason("Line empty"); }

            if (!text.StartsWith(QueryPhrase)) { return Reason("No leading '" + QueryPhrase + "'"); }
            text = StripLeading(text, QueryPhrase);
            text = StripTrailing(text, QuerySign);

            var tokens = Tokenize(text);

            var resourceName = tokens.Last();
            if (!Calculator.IsResource(resourceName)) { return Reason("This is an unknown resource: " + resourceName); }
            ResourceName = resourceName;

            tokens = StripTrailing(tokens, resourceName);

            string romanNumber = "";
            foreach (var token in tokens) {
                if (!Calculator.IsRomanDigitAlias(token)) { return Reason("This is an unknown number: " + token); }
                romanNumber += Calculator.GetRomanDigitByAlias(token);
            }

            if (!RomanNumber.TryToDecimal(romanNumber, out ArabNumber)) { return Reason("Converting roman number '" + romanNumber + "' to decimal failed"); }
            OriginalNumber = string.Join(" ", tokens);

            return true;
        }

        public void Execute()
        {
            Log.User(OriginalNumber + " " + ResourceName + " is " + GetTotalCredits() + " Credits");
        }

        public double GetTotalCredits()
        {
            return ArabNumber * Calculator.GetResourceValueInCredits(ResourceName);
        }

        public override string ToString()
        {
            return ArabNumber + " " + ResourceName;
        }
    }
}