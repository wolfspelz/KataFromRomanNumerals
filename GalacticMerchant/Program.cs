using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GalacticMerchant
{
    class Program
    {
        public class Config
        {
            public bool ShowHelp = false;
            public bool InteractiveSession = false;
            public string LogLevels = "User,Warning,Error";
        }

        static void Main(string[] args)
        {
            var config = new Config();

            var queue = new Queue<string>(args.ToList());
            while (queue.Count > 0) {
                switch (queue.Dequeue()) {
                    case "-i": config.InteractiveSession = true; break;
                    case "-l": config.LogLevels = queue.Dequeue(); break;
                    default: config.ShowHelp = true; break;
                }
            }

            if (config.ShowHelp) {
                Console.WriteLine("-i # Console interactive mode, 'quit' quits");
                Console.WriteLine("-l <log levels as comma separated list> # Any combination of Verbose,Info,User,Warning,Error (default: User,Warning,Error)");
                return;
            }

            if (config.InteractiveSession) {
                RunInteractive(config);
                return;
            }

            RunProblemDescriptionExample(config);
        }

        static void RunInteractive(Config config)
        {
            var calculator = new Calculator { Log = { Levels = config.LogLevels, Sink = ConsoleLogger } };

            while (true) {
                var line = Console.ReadLine();
                if (line == "quit") { return; }

                calculator.Parse(line).Execute();
            }
        }

        private static void RunProblemDescriptionExample(Config config)
        {
            var data =
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
how much wood could a woodchuck chuck if a woodchuck could chuck wood ?";

            var lines = data.Replace("\r\n", "\n").Split(new[] { '\n' });

            Console.WriteLine("Test input:");

            foreach (var line in lines) {
                Console.WriteLine(line);
            }

            Console.WriteLine("");

            var calculator = new Calculator { Log = { Levels = config.LogLevels, Sink = ConsoleLogger } };

            Console.WriteLine("Test output:");

            foreach (var line in lines) {
                var statement = calculator.Parse(line);
                statement.Execute();
            }

            Console.WriteLine("");

            Console.WriteLine("Press ENTER to quit");
            Console.ReadLine();
        }

        private static void ConsoleLogger(string logLevel, string logMessage)
        {
            switch (logLevel) {
                case "User": Console.WriteLine(logMessage); break;
                default: Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + " " + logLevel + " " + logMessage); break;
            }
        }
    }
}
