using System.Linq;
using TrelloNet;
using TrelloReleaseNotes.Core.Trello;

namespace TrelloReleaseNotes.Core.ReleaseTasks
{
    internal class ConsoleDump : ICardTask
    {
        public void DoWork(Card[] cards, ITrello trello, IDisplay display)
        {
            foreach (var card in cards.Select(x => new ReleaseNotesCard(x)))
            {
                display.Write(string.Format("{0} - {1}", card.Labels, card.Name));
            }
        }
    }
}