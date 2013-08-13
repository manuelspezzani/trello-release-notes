using System;
using System.Collections.Generic;
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
            var cards = FetchCardsOfSelectedList(trello, options.BoardId, options.List).ToArray();
            
            DumpCardsOnConsole(cards);

            GenerateReleaseNotes(options, cards);

            Console.WriteLine("Updating cards...");
            UpdateCards(trello, cards, options.SoftwareVersion);

            Console.WriteLine("\n\nRelease notes generated successfully!");
#if DEBUG
            System.Diagnostics.Process.Start(options.Output);
#endif
        }

        private static void UpdateCards(ITrello trello, Card[] cards, string softwareVersion)
        {
            foreach (var card in cards)
            {
                card.SetReleaseVersion(softwareVersion, trello);
            }
        }

        private static void DumpCardsOnConsole(Card[] cards)
        {
            foreach (var card in cards)
            {
                Console.WriteLine("{0} - {1}", card.Labels, card.Name);
            }
        }

        private static void GenerateReleaseNotes(Options options, Card[] cards)
        {
            var cardsGroupedByLabel = cards.GroupBy(x => x.Labels).ToArray();

            File.WriteAllText(options.Output,
                              Razor.Parse(File.ReadAllText(options.Template),
                                          new {options.SoftwareName, options.SoftwareVersion, Groups = cardsGroupedByLabel}));
        }

        private static IEnumerable<Card> FetchCardsOfSelectedList(ITrello trello, string boardId, string listId)
        {
            var board = trello.Boards.WithId(boardId);

            var list = trello.Lists
                .ForBoard(board)
                .First(x => string.Compare(x.Name, listId, StringComparison.InvariantCultureIgnoreCase) == 0);

            return trello.Cards.ForList(list).Select(x => new Card(x));
        }

        private static Options ParseArgumentsOrExit(string[] args, ITrello trello)
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
