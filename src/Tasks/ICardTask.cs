using TrelloNet;

namespace TrelloReleaseNotes.Tasks
{
    internal interface ICardTask
    {
        void DoWork(TrelloNet.Card[] cards, ITrello trello);
    }
}