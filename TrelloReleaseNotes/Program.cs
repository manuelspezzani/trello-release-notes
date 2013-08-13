using System;
using System.IO;
using System.Linq;
using RazorEngine;
using TrelloNet;

namespace TrelloReleaseNotes
{
    class Program
    {
        static void Main(string[] args)
        {
            var trello = new Trello(Constants.ApplicationId);

            var options = ParseArgumentsOrExit(args, trello);

            trello.Authorize(options.AuthorizationToken);

            Console.WriteLine("Fetching cards...\n");
            var cardsGroupedByLabel = FetchCardsOfList(trello, options.BoardId, options.List)
                                        .GroupBy(x => x.Labels);

            foreach (var group in cardsGroupedByLabel)
            {
                Console.WriteLine(string.Join(", ", group.Key));
                Console.WriteLine("------------------------------------");

                foreach (var card in group)
                {
                    Console.WriteLine(card.Name);
                    Console.WriteLine();
                }
            }

            File.WriteAllText(options.Output,
                              Razor.Parse(File.ReadAllText("DefaultTemplate\\template.html"), new { options.SoftwareName, options.SoftwareVersion, Groups = cardsGroupedByLabel }));

            Console.WriteLine("\n\nRelease notes generated successfully!");
#if DEBUG
            Console.ReadLine();
#endif
        }

        private static Card[] FetchCardsOfList(ITrello trello, string boardId, string listId)
        {
            var board = trello.Boards.WithId(boardId);

            var list = trello.Lists
                .ForBoard(board)
                .First(x => string.Compare(x.Name, listId, StringComparison.InvariantCultureIgnoreCase) == 0);

            return trello.Cards.ForList(list).Select(x => new Card(x)).ToArray();
        }

        private static Options ParseArgumentsOrExit(string[] args, Trello trello)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("To obtain an authorization token, please browse this URL:\n{0}",
                                  trello.GetAuthorizationUrl(Constants.ApplicationName, Scope.ReadWrite, Expiration.Never));
                Environment.Exit(-1);
            }
            return options;
        }
    }
}
