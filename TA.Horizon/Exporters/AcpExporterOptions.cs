using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TA.Horizon.Exporters
    {
    class AcpExporterOptions
        {
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
