using System.IO;
using System.Linq;
using RazorEngine;
using TrelloNet;
using TrelloReleaseNotes.Core.Trello;

namespace TrelloReleaseNotes.Core.ReleaseTasks
{
    internal class ReleaseNotes : ICardTask
    {
        private readonly IOptions _options;

        public ReleaseNotes(IOptions options)
        {
            _options = options;
        }

        public void DoWork(Card[] cards, ITrello trello, IDisplay display)
        {
            var cardsGroupedByLabel = cards
                                        .Select(x => new ReleaseNotesCard(x))
                                        .GroupBy(x => x.Labels)
                                        .ToArray();

            File.WriteAllText(_options.Output,
                              Razor.Parse(File.ReadAllText(_options.Template),
                                          new { _options.SoftwareName, _options.SoftwareVersion, Groups = cardsGroupedByLabel }));
        }
    }
}