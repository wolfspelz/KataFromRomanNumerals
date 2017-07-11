namespace GalacticMerchant.Statements
{
    public class RomanAliasDefinition : StatementBase, IStatement
    {
        private const string AssignmentToken = "is";

        private string Alias { get; set; }
        private string RomanDigit { get; set; }

        public RomanAliasDefinition(Calculator calculator) : base(calculator) { }

        public bool Parse(string text)
        {
            // like: glob is I
            if (string.IsNullOrEmpty(text)) { return Reason("Line empty"); }

            var tokens = Tokenize(text);

            if (tokens.Count != 3) { return Reason("Need 3 tokens"); }
            if (tokens[1] != AssignmentToken) { return Reason("The middle token must be '" + AssignmentToken + "'"); }

            var leftSide = tokens[0];
            var rightSide = tokens[2];
            if (!Calculator.IsRomanDigit(rightSide)) { return Reason("Not a roman digit"); }

            Alias = leftSide;
            RomanDigit = rightSide;

            return true;
        }

        public void Execute()
        {
            Calculator.LearnRomanDigitAlias(Alias, RomanDigit);
        }

        public override string ToString()
        {
            return Alias + " = " + RomanDigit;
        }
    }
}
