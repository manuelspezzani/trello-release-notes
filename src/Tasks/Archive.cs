using System;
using TrelloNet;

namespace TrelloReleaseNotes.Tasks
{
    internal class Archive : ICardTask
    {
        public void DoWork(Card[] cards, ITrello trello)
        {
            Console.WriteLine("Archiving cards...");

            foreach (var card in cards)
            {
                trello.Cards.Archive(card);
            }
        }
    }
}