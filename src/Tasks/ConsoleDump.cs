using System;
using System.Linq;
using TrelloNet;

namespace TrelloReleaseNotes.Tasks
{
    internal class ConsoleDump : ICardTask
    {
        public void DoWork(Card[] cards, ITrello trello)
        {
            foreach (var card in cards.Select(x => new ReleaseNotesCard(x)))
            {
                Console.WriteLine("{0} - {1}", card.Labels, card.Name);
            }
        }
    }
}