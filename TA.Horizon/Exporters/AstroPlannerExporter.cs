using System;
using System.IO;
using System.Linq;
using System.Text;
using CommandLine;

namespace TA.Horizon.Exporters
    {
    internal class AstroPlannerExporter : IHorizonExporter
        {
        ParserResult<AstroPlannerExporterOptions> options;

        public void ExportHorizon(HorizonData data)
            {
            using (var stream = new FileStream(options.Value.DestinationFile, FileMode.Create))
                {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                    writer.WriteLine("Azimuth,Lower,Light Dome");
                    for (int i = 0; i < 360; i += 5)
                        {
                        var lower = (int) data[i].HorizonAltitude;
                        var lightDome = (int) data[i].LightDomeAltitude;
                        writer.WriteLine("{0},{1},{2}", i, lower, lightDome);
                        }
                    writer.Close();
                    }
                }
            }

        public void ProcessCommandLineArguments(Parser parser, string[] args)
            {
            options = parser.ParseArguments<AstroPlannerExporterOptions>(args);
            if (options.Errors.Any())
                {
                Environment.ExitCode = -1;
                }
            }
        }
    }