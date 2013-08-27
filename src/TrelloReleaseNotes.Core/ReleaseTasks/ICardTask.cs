using TrelloNet;

namespace TrelloReleaseNotes.Core.ReleaseTasks
{
    internal interface ICardTask
    {
        void DoWork(Card[] cards, ITrello trello, IDisplay display);
    }
}