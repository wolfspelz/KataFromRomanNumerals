namespace GalacticMerchant.Statements
{
    public class RomanNumberQuery : StatementBase, IStatement
    {
        private const string QueryPhrase = "how much is";
        private const string QuerySign = "?";

        private string OriginalNumber = "";
        private int ArabNumber;

        public RomanNumberQuery(Calculator calculator) : base(calculator) { }

        public bool Parse(string text)
        {
            // like: how much is pish tegj glob glob ?
            if (string.IsNullOrEmpty(text)) { return Reason("Line empty"); }

            if (!text.StartsWith(QueryPhrase)) { return Reason("No leading '" + QueryPhrase + "'"); }
            text = StripLeading(text, QueryPhrase);
            text = StripTrailing(text, QuerySign);

            var tokens = Tokenize(text);

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
            Log.User(OriginalNumber + " is " + ArabNumber);
        }

        public override string ToString()
        {
            return OriginalNumber + " = " + ArabNumber;
        }
    }
}