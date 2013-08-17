namespace TrelloReleaseNotes.Core
{
    public interface IDisplay
    {
        void Write(string message);
        void Skip();
    }
}