using System;
using System.Collections.Generic;
using System.Linq;
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

            var tasks = new TaskBuilder().BuildFor(options);

            foreach (var cardTask in tasks)
            {
                cardTask.DoWork(cards, trello);
            }

            Console.WriteLine("\n\nRelease notes generated successfully!");
#if DEBUG
            System.Diagnostics.Process.Start(options.Output);
#endif
        }

        private static IEnumerable<TrelloNet.Card> FetchCardsOfSelectedList(ITrello trello, string boardId, string listId)
        {
            var board = trello.Boards.WithId(boardId);

            var list = trello.Lists
                .ForBoard(board)
                .First(x => string.Compare(x.Name, listId, StringComparison.InvariantCultureIgnoreCase) == 0);

            return trello.Cards.ForList(list);
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
