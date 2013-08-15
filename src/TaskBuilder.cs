using System.Collections.Generic;
using TrelloReleaseNotes.Tasks;

namespace TrelloReleaseNotes
{
    internal class TaskBuilder
    {
        public IEnumerable<ICardTask> BuildFor(Options options)
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