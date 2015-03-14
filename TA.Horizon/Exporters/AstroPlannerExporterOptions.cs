using CommandLine;

namespace TA.Horizon.Exporters
    {
    internal class AstroPlannerExporterOptions
        {
        [Option('d', "DestinationFile", Required = true,
            HelpText = "The destination file, into which the exported data is written in CSV format.")]
        public string DestinationFile { get; set; }
        }
    }