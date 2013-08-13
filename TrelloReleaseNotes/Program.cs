using System;
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

            var cardsGroupedByLabel = FetchCardsOfList(trello, options.BoardId, options.List)
                                        .GroupBy(x => string.Join(", ", x.Labels.Select(l => l.Name)));

            foreach (var group in cardsGroupedByLabel)
            {
                Console.WriteLine(string.Join(", ", group.Key));
                Console.WriteLine("------------------------------------");

                foreach (var card in group)
                {
                    Console.WriteLine(card.Name);
                    Console.WriteLine(card.Desc);
                    Console.WriteLine();
                }
            }

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

            return trello.Cards.ForList(list).ToArray();
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
