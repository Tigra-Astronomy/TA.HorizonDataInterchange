using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using JetBrains.Annotations;
using TA.Horizon.Importers;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Exporters
    {
    class AcpExporter : IHorizonExporter
        {
        readonly IRegistryWriter writer;
        internal AcpExporterOptions options = new AcpExporterOptions();
        string[] commandLineArguments;
        ParserResult<AcpExporterOptions> parseResult;

        [UsedImplicitly]
        public AcpExporter()
            {
            this.writer = new AcpRegistryReaderWriter();
            }
        internal AcpExporter(IRegistryWriter writer)
            {
            this.writer = writer;
            }

        public void ExportHorizon(HorizonData data)
            {
            var builder = new StringBuilder();
            for (int azimuth = 0; azimuth < 360; azimuth+=2)
                {
                var datum = data[azimuth];
                var altitude = datum.HorizonAltitude;
                if (options.UseLightDome)
                    altitude += datum.LightDomeAltitude;
                builder.AppendFormat("{0:F1} ", altitude);
                }
            builder.Length--;   // Removes the trailing space
            writer.SetKey("Horizon", builder);
            }

        public void ProcessCommandLineArguments(Parser parser, string[] args)
            {
            this.commandLineArguments = args;
            this.parseResult = parser.ParseArguments<AcpExporterOptions>(args);
            if (parseResult.Errors.Any())
                {
                Environment.ExitCode = -1;
                throw new ArgumentException("An error occurred processing the command line options.");
                }
            options = parseResult.Value;
            }
        }
    }
