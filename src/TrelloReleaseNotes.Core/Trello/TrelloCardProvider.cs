using System;
using System.Linq;
using TrelloNet;

namespace TrelloReleaseNotes.Core.Trello
{
    internal class TrelloCardProvider
    {
        public Card[] Fetch(ITrello trello, IOptions options)
        {
            var board = trello.Boards.WithId(options.BoardId);

            var list = trello.Lists
                .ForBoard(board)
                .First(x => string.Compare(x.Name, options.List, StringComparison.InvariantCultureIgnoreCase) == 0);

            return trello.Cards.ForList(list).ToArray();
        }
    }
}