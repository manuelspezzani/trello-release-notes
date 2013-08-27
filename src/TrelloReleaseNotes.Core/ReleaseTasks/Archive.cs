using TrelloNet;

namespace TrelloReleaseNotes.Core.ReleaseTasks
{
    internal class Archive : ICardTask
    {
        public void DoWork(Card[] cards, ITrello trello, IDisplay display)
        {
            display.Skip();
            display.Write("Archiving cards...");

            foreach (var card in cards)
            {
                trello.Cards.Archive(card);
            }
        }
    }
}