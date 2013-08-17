using System;
using TrelloNet;
using TrelloReleaseNotes.Core.Trello;

namespace TrelloReleaseNotes.Core
{
    public class Worker
    {
        private readonly TrelloNet.Trello _trello;
        private readonly TrelloCardProvider _cardProvider;
        private readonly TaskBuilder _taskBuilder;

        public Worker()
        {
            _trello = new TrelloNet.Trello(Constants.ApplicationId);
            _cardProvider = new TrelloCardProvider();
            _taskBuilder = new TaskBuilder();
        }

        public Uri GetAuthorizationUrl()
        {
            return _trello.GetAuthorizationUrl(Constants.ApplicationName, Scope.ReadWrite, Expiration.Never);
        }

        public void DoWork(IDisplay display, IOptions options)
        {
            _trello.Authorize(options.AuthorizationToken);

            display.Write("Fetching cards...");
            var cards = _cardProvider.Fetch(_trello, options);

            foreach (var cardTask in _taskBuilder.BuildFor(options))
            {
                cardTask.DoWork(cards, _trello, display);
            }

            display.Skip();
            display.Write("Release notes generated successfully!");
        }
    }
}