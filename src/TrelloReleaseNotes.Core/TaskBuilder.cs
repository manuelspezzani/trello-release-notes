using System.Collections.Generic;
using TrelloReleaseNotes.Core.ReleaseTasks;

namespace TrelloReleaseNotes.Core
{
    internal class TaskBuilder
    {
        internal IEnumerable<ICardTask> BuildFor(IOptions options)
        {
            yield return new ConsoleDump();
            yield return new ReleaseNotes(options);

            if (!options.Pretend)
            {
                yield return new SetReleaseVersion(options);

                if (options.Archive)
                    yield return new Archive();
            }
        }
    }
}