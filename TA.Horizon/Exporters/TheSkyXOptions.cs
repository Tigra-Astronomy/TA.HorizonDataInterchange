using CommandLine;

namespace TA.Horizon.Exporters
    {
    internal class TheSkyXExporterOptions
        {
        [Option('d', "DestinationFile", Required = true,
            HelpText = "The destination file, into which the exported data is written in HZN format.")]
        public string DestinationFile { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether light dome data (if present) should be used
        ///     when exporting the horizon. 
        /// </summary>
        /// <value><c>true</c> if [use light dome]; otherwise, <c>false</c>.</value>
        [Option('l', "UseLightDome", DefaultValue = false,
            HelpText = "Includes light dome data (if present) in the exported horizon.")]
        public bool UseLightDome { get; set; }
    }
}