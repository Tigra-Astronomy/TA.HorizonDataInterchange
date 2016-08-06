using System;
using System.IO;
using System.Linq;
using System.Text;
using CommandLine;

namespace TA.Horizon.Exporters
    {
    internal class TheSkyXExporter : IHorizonExporter
        {
        ParserResult<TheSkyXExporterOptions> options;

        public void ExportHorizon(HorizonData data)
            {
            using (var stream = new FileStream(options.Value.DestinationFile, FileMode.Create))
                {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                    writer.WriteLine("360");
                    const int TheSkyXAzimuthInterval = 1;
                    for (int azimuth = 0; azimuth < 360; azimuth += TheSkyXAzimuthInterval)
                        {
                            var datum = data[azimuth];
                            var altitude = datum.HorizonAltitude;
                            if (options.Value.UseLightDome)
                                altitude += datum.LightDomeAltitude;
                            writer.WriteLine("{0}", altitude);
                        }
                    writer.Close();
                    }
                }
            }


        public void ProcessCommandLineArguments(Parser parser, string[] args)
            {
            options = parser.ParseArguments<TheSkyXExporterOptions>(args);
            if (options.Errors.Any())
                {
                Environment.ExitCode = -1;
                }
            }
        }
    }