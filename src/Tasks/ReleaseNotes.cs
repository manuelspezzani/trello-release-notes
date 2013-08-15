using System.IO;
using System.Linq;
using RazorEngine;
using TrelloNet;

namespace TrelloReleaseNotes.Tasks
{
    internal class ReleaseNotes : ICardTask
    {
        private readonly Options _options;

        public ReleaseNotes(Options options)
        {
            _options = options;
        }

        public void DoWork(Card[] cards, ITrello trello)
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