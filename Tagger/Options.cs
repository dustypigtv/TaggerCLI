using CommandLine;

namespace Tagger
{
    class Options
    {
        [Option("metadata", Required = true)]
        public string Metadata { get; set; }

        [Option("mp4", Required = true)]
        public string Mp4 { get; set; }

    }
}
