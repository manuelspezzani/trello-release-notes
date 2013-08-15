using System;
using TrelloNet;

namespace TrelloReleaseNotes.Tasks
{
    internal class SetReleaseVersion : ICardTask
    {
        private readonly Options _options;

        public SetReleaseVersion(Options options)
        {
            _options = options;
        }

        public void DoWork(Card[] cards, ITrello trello)
        {
            Console.WriteLine();
            Console.WriteLine("Updating release version on cards...");

            foreach (var card in cards)
            {
                if (card.Desc.Contains(ReleaseTag(_options.SoftwareVersion)))
                    return;

                card.Desc += ReleaseTag(_options.SoftwareVersion);
                trello.Cards.ChangeDescription(card, card.Desc);
            }
        }

        private static string ReleaseTag(string softwareVersion)
        {
            return string.Format("\n\nRELEASED: {0}", softwareVersion);
        }
    }
}