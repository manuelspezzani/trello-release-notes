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

            var cards = FetchCards(trello, options);

            foreach (var group in cards.GroupBy(x => string.Join(", ", x.Labels.Select(l => l.Name))))
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

        private static Card[] FetchCards(Trello trello, Options options)
        {
            var board = trello.Boards.WithId(options.BoardId);

            var list = trello.Lists
                .ForBoard(board)
                .First(x => string.Compare(x.Name, options.List, StringComparison.InvariantCultureIgnoreCase) == 0);

            var cards = trello.Cards.ForList(list).ToArray();
            return cards;
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
