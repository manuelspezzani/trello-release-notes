using CommandLine;
using CommandLine.Text;
using TrelloReleaseNotes.Core;

namespace TrelloReleaseNotes
{
    public class Options : IOptions
    {
        [Option('s', "software", Required = true, HelpText = "Software name")]
        public string SoftwareName { get; set; }

        [Option('v', "version", Required = true, HelpText = "Software version to be released")]
        public string SoftwareVersion { get; set; }

        [Option('t', "token", Required = true, HelpText = "Authorization token")]
        public string AuthorizationToken { get; set; }

        [Option('b', "board", Required = true, HelpText = "Board ID")]
        public string BoardId { get; set; }
        
        [Option('l', "list", DefaultValue = "Done", HelpText = "Name of the list containing done cards")]
        public string List { get; set; }

        [Option('a', "archive", DefaultValue = false, HelpText = "Done cards will be archived")]
        public bool Archive { get; set; }

        [Option('o', "output", HelpText = "Output file to generate", DefaultValue = "releaseNotes.html")]
        public string Output { get; set; }

        [Option("template", HelpText = "Template file to be used for release notes generation", DefaultValue = "default_template.html")]
        public string Template { get; set; }

        [Option("pretend", HelpText = "Simulates the execution", DefaultValue = false)]
        public bool Pretend { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var usage = HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));

            return usage;
        }
    }
}