using System;
using Microsoft.Build.Utilities;

namespace TrelloReleaseNotes.Core.MsBuild
{
    public class TrelloRelease : Task, IOptions, IDisplay
    {
        public string SoftwareName { get; private set; }
        public string SoftwareVersion { get; private set; }
        public string AuthorizationToken { get; private set; }
        public string BoardId { get; private set; }
        public string List { get; private set; }
        public bool Archive { get; private set; }
        public string Output { get; private set; }
        public string Template { get; private set; }
        public bool Pretend { get; private set; }

        public override bool Execute()
        {
            try
            {
                var worker = new Worker();
                worker.DoWork(this, this);
                return true;
            }
            catch (Exception exception)
            {
                Log.LogErrorFromException(exception);
                return false;
            }
        }
        
        public void Write(string message)
        {
            Log.LogMessage(message);
        }

        public void Skip()
        {
        }
    }
}