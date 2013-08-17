using System.Linq;
using MarkdownSharp;
using TrelloNet;

namespace TrelloReleaseNotes.Core.Trello
{
    public class ReleaseNotesCard
    {
        private readonly Card _card;
        private readonly Markdown _renderer;

        public ReleaseNotesCard(Card card)
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
    }
}