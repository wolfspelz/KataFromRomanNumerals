namespace GalacticMerchant.Statements
{
    public class AnyOtherStatement : StatementBase, IStatement
    {
        private string UnhandledText { get; set; }

        public AnyOtherStatement(Calculator calculator) : base(calculator) {}

        public bool Parse(string text)
        {
            UnhandledText = text;
            return true;
        }

        public void Execute()
        {
            if (!string.IsNullOrEmpty(UnhandledText.Trim())) {
                Log.User("I have no idea what you are talking about");
                return;
            }

            Log.Info("Ignored: (" + UnhandledText + ")");
        }

        public override string ToString()
        {
            return "?";
        }
    }
}
