using CommandLine;

namespace TA.Horizon.Exporters
    {
    internal class ObserverProExporterOptions
        {
        [Option('d', "DestinationFile", Required = true,
            HelpText = "The destination file, into which the exported data is written in HZN format.")]
        public string DestinationFile { get; set; }
        }
    }