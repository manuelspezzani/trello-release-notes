using System;
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

        [Option('l', "list", DefaultValue = "Done", HelpText = "Name of the list containing done cards")]
        public string List { get; set; }

        [Option('s', "software", HelpText = "Software name")]
        public string SoftwareName { get; set; }

        [Option('v', "version", Required = true, HelpText = "Software version to be released")]
        public string SoftwareVersion { get; set; }

        [Option('p', "pretend", HelpText = "Simulates the execution", DefaultValue = false)]
        public bool Pretend { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file to generate", DefaultValue = "out.txt")]
        public string Output { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var usage = HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));

            return usage;
        }
    }
}