namespace GalacticMerchant
{
    public interface IStatement
    {
        bool Parse(string text);
        void Execute();

        string GetReason();
    }
}
