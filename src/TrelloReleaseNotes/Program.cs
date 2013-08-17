using System;
using TrelloReleaseNotes.Core;

namespace TrelloReleaseNotes
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new Worker();
            var options = ParseArgumentsOrExit(args, worker);

            worker.DoWork(new ConsoleDisplay(), options);

#if DEBUG
            System.Diagnostics.Process.Start(options.Output);
#endif
        }

        private static Options ParseArgumentsOrExit(string[] args, Worker worker)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("To obtain an authorization token, please browse this URL:\n{0}",
                                  worker.GetAuthorizationUrl());
                Environment.Exit(-1);
            }
            return options;
        }
    }
}
