using TrelloNet;

namespace TrelloReleaseNotes.Core.ReleaseTasks
{
    internal class SetReleaseVersion : ICardTask
    {
        private readonly IOptions _options;

        public SetReleaseVersion(IOptions options)
        {
            _options = options;
        }

        public void DoWork(Card[] cards, ITrello trello, IDisplay display)
        {
            display.Skip();
            display.Write("Updating release version on cards...");

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