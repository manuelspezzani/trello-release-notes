using System.Linq;
using MarkdownSharp;

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

        public string Description
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