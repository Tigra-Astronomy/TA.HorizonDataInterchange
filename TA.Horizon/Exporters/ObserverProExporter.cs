using System;
using System.IO;
using System.Linq;
using System.Text;
using CommandLine;

namespace TA.Horizon.Exporters
    {
    internal class ObserverProExporter : IHorizonExporter
        {
        ParserResult<ObserverProExporterOptions> options;

        public void ExportHorizon(HorizonData data)
            {
            using (var stream = new FileStream(options.Value.DestinationFile, FileMode.Create))
                {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                    // TODO Need to fix this so that the flag for -UseLightDome is interpreted and that value is output
                    //writer.WriteLine("Azimuth,Lower,Light Dome");
                        const int ObserverProAzimuthInterval = 1;
                    //    for (int i = 0; i < 360; i += ObserverProAzimuthInterval)
                    //    {
                    //    var lower = data[i].HorizonAltitude;
                    // var lightDome = (int) data[i].LightDomeAltitude;
                    // writer.WriteLine("{0},{1},{2}", i, lower, lightDome);
                    //    writer.WriteLine("{0},{1}", i, lower);
                        //var builder = new StringBuilder();
                        for (int azimuth = 0; azimuth < 360; azimuth += ObserverProAzimuthInterval)
                        {
                            var datum = data[azimuth];
                            var altitude = datum.HorizonAltitude;
                            if (options.Value.UseLightDome)
                                altitude += datum.LightDomeAltitude;
                        writer.WriteLine("{0},{1}", azimuth, altitude);
                        //builder.AppendFormat("{0:F1} ", altitude);

                            //builder.Length--; // Removes the trailing space
                            //writer.SetKey("Horizon", builder);
                        }
                        writer.Close();
                    }
                }
            }


        public void ProcessCommandLineArguments(Parser parser, string[] args)
            {
            options = parser.ParseArguments<ObserverProExporterOptions>(args);
            if (options.Errors.Any())
                {
                Environment.ExitCode = -1;
                }
            }
        }
    }