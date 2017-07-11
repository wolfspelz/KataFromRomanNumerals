namespace GalacticMerchant
{
    public class ComponentLogger
    {
        public delegate void Callback(string level, string message);

        public static void DevNull(string level, string message) { }

        public void Verbose(string message) { _Log("Verbose", message); }
        public void Info(string message) { _Log("Info", message); }
        public void User(string message) { _Log("User", message); }
        public void Warning(string message) { _Log("Warning", message); }
        public void Error(string message) { _Log("Error", message); }

        public Callback Sink = DevNull;
        public string Levels = "User,Warning,Error";

        private void _Log(string level, string message)
        {
            if (Sink != null && !string.IsNullOrEmpty(Levels) && Levels.Contains(level)) {
                Sink(level, message);
            }
        }

    }
}
