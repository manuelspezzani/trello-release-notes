using CommandLine;
using CommandLine.Text;

namespace TrelloReleaseNotes
{
    class Options
    {
        [Option('b', "board", Required = true, HelpText = "Board ID")]
        public string BoardId { get; set; }

        [Option('t', "token", Required = true, HelpText = "Authorization token")]
        public string AuthorizationToken { get; set; }

        [Option('s', "software", Required = true, HelpText = "Software name")]
        public string SoftwareName { get; set; }

        [Option('v', "version", Required = true, HelpText = "Software version to be released")]
        public string SoftwareVersion { get; set; }
        
        [Option('l', "list", DefaultValue = "Done", HelpText = "Name of the list containing done cards")]
        public string List { get; set; }

        [Option('p', "pretend", HelpText = "Simulates the execution", DefaultValue = false)]
        public bool Pretend { get; set; }

        [Option('o', "output", HelpText = "Output file to generate", DefaultValue = "releaseNotes.html")]
        public string Output { get; set; }

        [Option("template", HelpText = "Template file to be used for release notes generation", DefaultValue = "default_template.html")]
        public string Template { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var usage = HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));

            return usage;
        }
    }
}