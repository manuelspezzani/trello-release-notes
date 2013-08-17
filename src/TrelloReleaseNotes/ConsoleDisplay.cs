using System;
using TrelloReleaseNotes.Core;

namespace TrelloReleaseNotes
{
    internal class ConsoleDisplay : IDisplay
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void Skip()
        {
            Console.WriteLine();
        }
    }
}