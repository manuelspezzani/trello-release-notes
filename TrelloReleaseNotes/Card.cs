using System.Linq;
using MarkdownSharp;
using TrelloNet;

namespace TrelloReleaseNotes
{
    public class Card
    {
        private readonly TrelloNet.Card _card;
        private readonly Markdown _renderer;

        public Card(TrelloNet.Card card)
        {
            _card = card;
            _renderer = new Markdown();
        }

        public string Name
        {
            get { return _card.Name; }
        }

        public string HtmlDescription
        {
            get
            {
                return _renderer.Transform(_card.Desc);
            }
        }

        public string Labels
        {
            get { return string.Join(", ", _card.Labels.Select(x => x.Name)); }
        }

        public void SetReleaseVersion(string softwareVersion, ITrello trello)
        {
            if (_card.Desc.Contains(ReleaseTag(softwareVersion)))
                return;

            _card.Desc += ReleaseTag(softwareVersion);
            trello.Cards.ChangeDescription(_card, _card.Desc);
        }

        private static string ReleaseTag(string softwareVersion)
        {
            return string.Format("\n\nRELEASED: {0}", softwareVersion);
        }
    }
}