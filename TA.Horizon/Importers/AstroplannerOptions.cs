using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TA.Horizon.Importers
    {
    sealed class AstroplannerOptions
        {
        [Option('s',"SourceFile", Required=true, HelpText = "The CSV file name.")]
        public string SourceFile { get; set; }
        }
    }
