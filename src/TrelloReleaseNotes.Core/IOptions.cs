namespace TrelloReleaseNotes.Core
{
    public interface IOptions
    {
        string SoftwareName { get; }
        string SoftwareVersion { get; }
        string AuthorizationToken { get; }
        string BoardId { get; }
        string List { get; }
        bool Archive { get; }
        string Output { get; }
        string Template { get; }
        bool Pretend { get; }
    }
}